using CourseService.DataAccess.Models;


namespace CourseService.Domain.Interfaces
{
    public interface ICourseService
    {
       Task<List<Course>> GetAllCourses();
        Task<Course>GetCourseById(int courseId);
        Task<Course> AddCourse(Course course);
        Task<Course> UpdateCourse(Course course);
        Task<Course> DeleteCourse(int courseId);

    }
}
