using CourseService.Constant;
using CourseService.DataAccess.Models;

using CourseService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route(RouteMapConstants.BaseControllerRoute)]
    public class CoursesControler : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesControler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #region Course Endpoints

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCourses();
            return result != null ? Ok(result) : NotFound();
        }


        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
         var result = await _courseService.GetCourseById(courseId);
            return result != null ? Ok() : NotFound($"ID {courseId} is not found");
        }

        
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            var result = await _courseService.AddCourse(course);
                return result != null ? Ok(result) : NotFound();
        }

      
        [HttpPut("")]
        public async Task<IActionResult> UpdateCourse([FromBody] Course course)
        {
            var result = await _courseService.UpdateCourse(course);
            return result != null ? Ok(result) : NotFound("Id is Not Found");
        }

     
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var result = await _courseService.DeleteCourse(courseId);

           if (result != null)
                return Ok($"ID {courseId} is deleted");
            else
                return NotFound($"ID {courseId} is not found");
        }

    }
}
#endregion
