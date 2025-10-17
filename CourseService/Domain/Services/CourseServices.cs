using CourseService.DataAccess.Models;
using CourseService.Domain.DTO;
using CourseService.Domain.Interfaces;

namespace CourseService.Domain.Services
{
    public class CourseServices : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<(List<Course> Courses, WebAPIErrorMessage Error)> GetAllCourses()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> GetCourseById(int courseId)
        {
            return await _courseRepository.GetCourseByIdAsync(courseId);
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> AddCourse(Course course)
        {
            return await _courseRepository.AddCourseAsync(course);
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> UpdateCourse(Course course)
        {
            return await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> DeleteCourse(int courseId)
        {
            return await _courseRepository.DeleteCourseAsync(courseId);
        }

        public async Task<(List<Topic> Topics, WebAPIErrorMessage Error)> GetAllTopics()
        {
            return await _courseRepository.GetAllTopicsAsync();
        }

        public async Task<(Topic Topic, WebAPIErrorMessage Error)> GetTopicById(int topicId)
        {
            return await _courseRepository.GetTopicByIdAsync(topicId);
        }

        public async Task<(Topic Topic, WebAPIErrorMessage Error)> AddTopic(Topic topic)
        {
            return await _courseRepository.AddTopicAsync(topic);
        }
    }
}
