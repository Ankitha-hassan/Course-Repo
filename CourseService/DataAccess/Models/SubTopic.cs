using System.ComponentModel.DataAnnotations;

namespace CourseService.DataAccess.Models
{
    public class SubTopic
    {
        [Key]
        public int SubTopicId { get; set; }

        public int TopicId { get; set; }

        public string SubTopicName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
