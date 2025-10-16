using CourseService.DataAccess.DBContext;
using CourseService.DataAccess.Models;
using CourseService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Domain.Repository
{
    public class CourseRepository:ICourseRepository
    {
       public readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task<Course> GetCourseById(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            return course;
        }
        public async Task<Course> AddCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }
        public async Task<Course> UpdateCourse(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.CourseId);
            if (existingCourse == null)
            {
                return null; 
            }
            existingCourse.CourseName = course.CourseName;
            existingCourse.Description = course.Description;
            await _context.SaveChangesAsync();
            return existingCourse;
        }
        public async Task<Course> DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return null;
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return course;
        }

    }
}
