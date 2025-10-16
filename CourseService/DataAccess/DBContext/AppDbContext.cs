
using CourseService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseService.DataAccess.DBContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<SubTopic> SubTopics { get; set; } = null!;

    }
}
