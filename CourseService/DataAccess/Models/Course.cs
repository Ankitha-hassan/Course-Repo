using CourseService.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseService.DataAccess.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        public string CourseName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public List<Topic> Topics { get; set; } = new List<Topic>();
    }
}
