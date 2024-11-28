using System.ComponentModel.DataAnnotations;

namespace UniversityForumAPI.DTOs.TopicDTOs
{
    public class CreateTopicDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }
    }
}
