using CourseService.DataAccess.Models;
using CourseService.Domain.DTO;

namespace CourseService.Domain.Interfaces
{
    public interface ICourseService
    {
        Task<(List<Course> Courses, WebAPIErrorMessage Error)> GetAllCourses();
        Task<(Course Course, WebAPIErrorMessage Error)> GetCourseById(int courseId);
        Task<(Course Course, WebAPIErrorMessage Error)> AddCourse(Course course);
        Task<(Course Course, WebAPIErrorMessage Error)> UpdateCourse(Course course);
        Task<(Course Course, WebAPIErrorMessage Error)> DeleteCourse(int courseId);

        // For topics
        Task<(List<Topic> Topics, WebAPIErrorMessage Error)> GetAllTopics();
        Task<(Topic Topic, WebAPIErrorMessage Error)> GetTopicById(int topicId);
        Task<(Topic Topic, WebAPIErrorMessage Error)> AddTopic(Topic topic);
    }
}
