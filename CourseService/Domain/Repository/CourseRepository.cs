using CourseService.DataAccess.DBContext;
using CourseService.DataAccess.Models;
using CourseService.Domain.DTO;
using CourseService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Domain.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public CourseRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        #region Course
        public async Task<(List<Course> Courses, WebAPIErrorMessage Error)> GetAllCoursesAsync()
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var courses = await context.Courses.ToListAsync();
                    return (courses, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error retrieving courses: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> GetCourseByIdAsync(int courseId)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var course = await context.Courses.FindAsync(courseId);
                    if (course == null)
                    {
                        return (null, new WebAPIErrorMessage
                        {
                            Message = "Course not found with the given ID."
                        });
                    }
                    return (course, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error retrieving course: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> AddCourseAsync(Course course)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Courses.Add(course);
                    await context.SaveChangesAsync();
                    return (course, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error adding course: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> UpdateCourseAsync(Course course)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var existingCourse = await context.Courses.FindAsync(course.CourseId);
                    if (existingCourse == null)
                    {
                        return (null, new WebAPIErrorMessage
                        {
                            Message = "Course not found."
                        });
                    }

                    existingCourse.CourseName = course.CourseName;
                    existingCourse.Description = course.Description;
                    await context.SaveChangesAsync();
                    return (existingCourse, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error updating course: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }

        public async Task<(Course Course, WebAPIErrorMessage Error)> DeleteCourseAsync(int courseId)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var course = await context.Courses.FindAsync(courseId);
                    if (course == null)
                    {
                        return (null, new WebAPIErrorMessage
                        {
                            Message = "Course not found."
                        });
                    }

                    context.Courses.Remove(course);
                    await context.SaveChangesAsync();
                    return (course, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error deleting course: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }
        #endregion
        #region Topic
        public async Task<(List<Topic> Topics, WebAPIErrorMessage Error)> GetAllTopicsAsync()
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var topics = await context.Topics.ToListAsync();
                    return (topics, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error retrieving topics: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }
        public async Task<(Topic Topic, WebAPIErrorMessage Error)> GetTopicByIdAsync(int topicId)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var topic = await context.Topics.FindAsync(topicId);
                    if (topic == null)
                    {
                        return (null, new WebAPIErrorMessage
                        {
                            Message = "Topic not found with the given ID."
                        });
                    }
                    return (topic, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error retrieving topic: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }

        }
        public async Task<(Topic Topic, WebAPIErrorMessage Error)> AddTopicAsync(Topic topic)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Topics.Add(topic);
                    await context.SaveChangesAsync();
                    return (topic, null);
                }
            }
            catch (Exception ex)
            {
                return (null, new WebAPIErrorMessage
                {
                    Message = $"Error adding topic: {ex.Message}",
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
    #endregion
}