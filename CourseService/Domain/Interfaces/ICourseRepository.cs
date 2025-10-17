using CourseService.DataAccess.Models;
using CourseService.Domain.DTO;

namespace CourseService.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<(List<Course> Courses, WebAPIErrorMessage Error)> GetAllCoursesAsync();
        Task<(Course Course, WebAPIErrorMessage Error)> GetCourseByIdAsync(int courseId);
        Task<(Course Course, WebAPIErrorMessage Error)> AddCourseAsync(Course course);
        Task<(Course Course, WebAPIErrorMessage Error)> UpdateCourseAsync(Course course);
        Task<(Course Course, WebAPIErrorMessage Error)> DeleteCourseAsync(int courseId);


        //Topic
      Task<(List<Topic> Topics, WebAPIErrorMessage Error)> GetAllTopicsAsync();
        Task<(Topic Topic, WebAPIErrorMessage Error)> GetTopicByIdAsync(int topicId);
        Task<(Topic Topic, WebAPIErrorMessage Error)> AddTopicAsync(Topic topic);
    }
}
