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
        Task<(Topic Topic, WebAPIErrorMessage Error)> UpdateTopic(Topic topic);
        Task<(Topic Topic, WebAPIErrorMessage Error)> DeleteTopic(int topicId);


        // SubTopic operations
        Task<(List<SubTopic> subTopics, WebAPIErrorMessage Error)> GetSubTopicsByTopicId(int topicId);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> GetSubTopicById(int subTopicId);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> AddSubTopic(SubTopic subTopic);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> UpdateSubTopic(SubTopic subTopic);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> DeleteSubTopic(int subTopicId);
    }
}
