using CourseService.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http.Json;

namespace CourseService.Controllers
{
    [Route(RouteMapConstants.BaseControllerRoute)]
    [ApiController]
    public class IdeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IdeController> _logger;

        // Simple in-memory rate limiting
        private static readonly Dictionary<string, DateTime> _rateLimitCache = new();
        private const int RateLimitSeconds = 5;

        public IdeController(IHttpClientFactory httpClientFactory, ILogger<IdeController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Execute code in various programming languages
        /// </summary>
        [HttpPost("run")]
        public async Task<IActionResult> RunCode([FromBody] CodeRequest request)
        {
            // ✅ DEBUG LOGGING
            _logger.LogInformation("=== RECEIVED REQUEST ===");
            _logger.LogInformation("Language: {Language}", request.Language);
            _logger.LogInformation("Code length: {Length}", request.Code?.Length ?? 0);
            _logger.LogInformation("Stdin provided: {HasStdin}", !string.IsNullOrEmpty(request.Stdin));
            _logger.LogInformation("Stdin content: {Stdin}", request.Stdin ?? "(null)");
            _logger.LogInformation("========================");

            // Input validation
            if (request == null || string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Language))
            {
                return BadRequest(new { error = "Code and Language are required." });
            }

            // Basic code length validation
            if (request.Code.Length > 10000)
            {
                return BadRequest(new { error = "Code exceeds maximum length of 10,000 characters." });
            }

            // Rate limiting
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            if (_rateLimitCache.TryGetValue(clientIp, out var lastRequest))
            {
                if ((DateTime.UtcNow - lastRequest).TotalSeconds < RateLimitSeconds)
                {
                    return StatusCode(429, new { error = "Too many requests. Please wait a moment." });
                }
            }
            _rateLimitCache[clientIp] = DateTime.UtcNow;

            // Clean up old entries (simple cleanup every 100 requests)
            if (_rateLimitCache.Count > 100)
            {
                var expiredKeys = _rateLimitCache
                    .Where(x => (DateTime.UtcNow - x.Value).TotalMinutes > 5)
                    .Select(x => x.Key)
                    .ToList();

                foreach (var key in expiredKeys)
                {
                    _rateLimitCache.Remove(key);
                }
            }

            try
            {
                string output = request.Language.ToLower() switch
                {
                    "python" => await ExecutePiston(request.Code, "python", "3.10.0", request.Stdin),
                    "java" => await ExecutePiston(request.Code, "java", "15.0.2", request.Stdin),
                    "csharp" => await ExecutePiston(request.Code, "csharp", "6.12.0", request.Stdin),
                    "javascript" => await ExecutePiston(request.Code, "javascript", "18.15.0", request.Stdin),
                    _ => throw new NotSupportedException($"Language '{request.Language}' is not supported.")
                };

                return Ok(new { output });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error executing code for language: {Language}", request.Language);
                return StatusCode(503, new { error = "Code execution service unavailable. Please try again later." });
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout executing code for language: {Language}", request.Language);
                return StatusCode(408, new { error = "Code execution timed out. Please optimize your code or try again." });
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error executing code for language: {Language}", request.Language);
                return StatusCode(500, new { error = "An error occurred while executing your code. Please try again." });
            }
        }

     
        /// Execute code using Piston API
       private async Task<string> ExecutePiston(string code, string language, string version, string? stdin = null)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            var fileName = language switch
            {
                "python" => "main.py",
                "java" => "Main.java",
                "csharp" => "Main.cs",
                "javascript" => "main.js",
                _ => "main.txt"
            };

            var payload = new
            {
                language,
                version,
                files = new[]
                {
                    new { name = fileName, content = code }
                },
                stdin = stdin ?? string.Empty // Include stdin for input support
            };

            try
            {
                var response = await httpClient.PostAsJsonAsync("https://emkc.org/api/v2/piston/execute", payload);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Piston API returned status {StatusCode}: {Error}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Code execution service returned status code: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<PistonResponse>();

                if (result?.Run == null)
                {
                    return "No output received from execution service.";
                }

                // Combine stdout and stderr for complete output
                var output = string.Empty;

                if (!string.IsNullOrEmpty(result.Run.Stdout))
                {
                    output = result.Run.Stdout;
                }

                if (!string.IsNullOrEmpty(result.Run.Stderr))
                {
                    output += (string.IsNullOrEmpty(output) ? "" : "\n") + result.Run.Stderr;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "Code executed successfully with no output.";
                }

                return output;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Piston API");
                throw new HttpRequestException("Failed to communicate with code execution service.", ex);
            }
        }

       
        [HttpGet("languages")]
        public IActionResult GetSupportedLanguages()
        {
            var languages = new[]
            {
                new { id = "python", name = "Python", version = "3.10.0", extension = ".py" },
                new { id = "java", name = "Java", version = "15.0.2", extension = ".java" },
                new { id = "csharp", name = "C#", version = "6.12.0", extension = ".cs" },
                new { id = "javascript", name = "JavaScript", version = "18.15.0", extension = ".js" }
            };

            return Ok(languages);
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }

        #region Models

        public class PistonResponse
        {
            public RunResult? Run { get; set; }

            public class RunResult
            {
                public string? Stdout { get; set; }
                public string? Stderr { get; set; }
                public string? Output { get; set; }
                public int Code { get; set; }
            }
        }

        // ✅ UPDATED: Added Stdin property
        public class CodeRequest
        {
            public string Language { get; set; } = "python";
            public string Code { get; set; } = string.Empty;
            public string? Stdin { get; set; } = null;
        }

        #endregion
    }
}