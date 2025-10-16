using CourseService.DataAccess.Models;
using CourseService.Domain.Interfaces;

namespace CourseService.Domain.Services
{
    public class CourseServices:ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCourses();
        }
        public async Task<Course> GetCourseById(int courseId)
        {
            return await _courseRepository.GetCourseById(courseId);
        }
        public async Task<Course> AddCourse(Course course)
        {
            return await _courseRepository.AddCourse(course);
        }
        public async Task<Course> UpdateCourse(Course course)
        {
            return await _courseRepository.UpdateCourse(course);
        }
        public async Task<Course> DeleteCourse(int courseId)
        {
            return await _courseRepository.DeleteCourse(courseId);
        }

    }
}
