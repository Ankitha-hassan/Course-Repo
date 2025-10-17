namespace CourseService.Domain.DTO
{
    public class WebAPIErrorMessage
    {
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; } = string.Empty;
    }
}
