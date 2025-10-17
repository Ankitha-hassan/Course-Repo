using CourseService.Constant;
using CourseService.DataAccess.Models;
using CourseService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route(RouteMapConstants.BaseControllerRoute)]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #region Course Endpoints

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var (courses, error) = await _courseService.GetAllCourses();
            if (error != null)
                return StatusCode(500, error);

            if (courses == null || !courses.Any())
                return NotFound("No courses found");

            return Ok(courses);
        }

        [HttpGet(RouteMapConstants.GetCourseById)]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var (course, error) = await _courseService.GetCourseById(courseId);
            if (error != null)
                return StatusCode(500, error);

            if (course == null)
                return NotFound($"Course with ID {courseId} not found");

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            if (course == null)
                return BadRequest("Course data is required");

            var (created, error) = await _courseService.AddCourse(course);
            if (error != null)
                return StatusCode(500, error);

            return CreatedAtAction(nameof(GetCourseById), new { courseId = created.CourseId }, created);
        }

        [HttpPut(RouteMapConstants.UpdateCourseById)]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] Course course)
        {
            if (course == null)
                return BadRequest("Course data is required");

            course.CourseId = courseId;
            var (updated, error) = await _courseService.UpdateCourse(course);

            if (error != null)
                return StatusCode(500, error);

            if (updated == null)
                return NotFound($"Course with ID {courseId} not found");

            return Ok(updated);
        }

        [HttpDelete(RouteMapConstants.DeleteCourseById)]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var (deleted, error) = await _courseService.DeleteCourse(courseId);

            if (error != null)
                return StatusCode(500, error);

            if (deleted == null)
                return NotFound($"Course with ID {courseId} not found");

            return Ok($"Course with ID {courseId} was deleted successfully");
        }

        #endregion

        #region Topic Endpoints

        [HttpGet(RouteMapConstants.GetAllTopicsByCourseId)]
        public async Task<IActionResult> GetAllTopics()
        {
            var (topics, error) = await _courseService.GetAllTopics();
            if (error != null)
                return StatusCode(500, error);

            if (topics == null || !topics.Any())
                return NotFound("No topics found");

            return Ok(topics);
        }

       
        [HttpGet(RouteMapConstants.GetTopicById)]
        public async Task<IActionResult> GetTopicById(int courseId)
        {
            var (topic, error) = await _courseService.GetTopicById(courseId);
            if (error != null)
                return StatusCode(500, error);

            if (topic == null)
                return NotFound($"Topic with ID {courseId} not found");

            return Ok(topic);
        }

        [HttpPost(RouteMapConstants.AddTopic)]
        public async Task<IActionResult> AddTopic([FromBody] Topic topic)
        {
            if (topic == null)
                return BadRequest("Topic data is required");

            var (created, error) = await _courseService.AddTopic(topic);
            if (error != null)
                return StatusCode(500, error);

            return Ok(created);
        }

        [HttpPut(RouteMapConstants.UpdateTopicById)]
        public async Task<IActionResult> UpdateTopic(int topicId, [FromBody] Topic topic)
        {
            if (topic == null)
                return BadRequest("Topic data is required");

            topic.TopicId = topicId;

            var (updated, error) = await _courseService.UpdateTopic(topic);

            if (error != null)
                return StatusCode(500, error);

            if (updated == null)
                return NotFound($"Topic with ID {topicId} not found");

            return Ok(updated);
        }

        [HttpDelete(RouteMapConstants.DeleteTopicById)]
        public async Task<IActionResult> DeleteTopic(int topicId)
        {
            var (deleted, error) = await _courseService.DeleteTopic(topicId);

            if (error != null)
                return StatusCode(500, error);

            if (deleted == null)
                return NotFound($"Topic with ID {topicId} not found");

            return Ok($"Topic with ID {topicId} was deleted successfully");
        }


        #endregion

        #region Subtopics Endpoints

        [HttpGet(RouteMapConstants.GetSubTopicsByTopicId)]
        public async Task<IActionResult> GetSubTopicsByTopicId(int topicId)
        {
            var (subtopics, error) = await _courseService.GetSubTopicsByTopicId(topicId);

            if (error != null)
                return StatusCode(500, error);

            if (subtopics == null || !subtopics.Any())
                return NotFound($"No subtopics found for Topic ID {topicId}");

            return Ok(subtopics);
        }

        [HttpGet(RouteMapConstants.GetSubTopicById)]
        public async Task<IActionResult> GetSubTopicById(int subTopicId)
        {
            var (subtopic, error) = await _courseService.GetSubTopicById(subTopicId);

            if (error != null)
                return StatusCode(500, error);

            if (subtopic == null)
                return NotFound($"Subtopic with ID {subTopicId} not found");

            return Ok(subtopic);
        }

        [HttpPost(RouteMapConstants.AddSubTopicByTopicID)]
        public async Task<IActionResult> AddSubTopic(int topicId, [FromBody] SubTopic subTopic)
        {
            if (subTopic == null)
                return BadRequest("Subtopic data is required");

            subTopic.TopicId = topicId;
            var (created, error) = await _courseService.AddSubTopic(subTopic);

            if (error != null)
                return StatusCode(500, error);

            return CreatedAtAction(nameof(GetSubTopicById), new { subTopicId = created.SubTopicId }, created);
        }

        [HttpPut(RouteMapConstants.UpdateSubTopicById)]
        public async Task<IActionResult> UpdateSubTopic(int subTopicId, [FromBody] SubTopic subTopic)
        {
            if (subTopic == null)
                return BadRequest("Subtopic data is required");

            subTopic.SubTopicId = subTopicId;
            var (updated, error) = await _courseService.UpdateSubTopic(subTopic);

            if (error != null)
                return StatusCode(500, error);

            if (updated == null)
                return NotFound($"Subtopic with ID {subTopicId} not found");

            return Ok(updated);
        }

        [HttpDelete(RouteMapConstants.DeleteSubTopicById)]
        public async Task<IActionResult> DeleteSubTopic(int subTopicId)
        {
            var (deleted, error) = await _courseService.DeleteSubTopic(subTopicId);

            if (error != null)
                return StatusCode(500, error);

            if (deleted == null)
                return NotFound($"Subtopic with ID {subTopicId} not found");

            return Ok($"Subtopic with ID {subTopicId} was deleted successfully");
        }

        #endregion
    }
}
