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
        Task<(Topic Topic, WebAPIErrorMessage Error)> UpdateTopicAsync(Topic topic);
        Task<(Topic Topic, WebAPIErrorMessage Error)> DeleteTopicAsync(int topicId);


        // Subtopics
        Task<(List<SubTopic> subTopics, WebAPIErrorMessage Error)> GetSubTopicsByTopicId(int topicId);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> GetSubTopicById(int subTopicId);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> AddSubTopic(SubTopic subTopic);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> UpdateSubTopic(SubTopic subTopic);
        Task<(SubTopic subTopics, WebAPIErrorMessage Error)> DeleteSubTopic(int subTopicId);
    }
}
