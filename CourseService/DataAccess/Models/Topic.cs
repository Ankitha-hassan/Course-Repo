using System.ComponentModel.DataAnnotations;

namespace CourseService.DataAccess.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        public int CourseId { get; set; }

        public string TopicName { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public List<SubTopic> SubTopics { get; set; } = new List<SubTopic>();
    }
}
