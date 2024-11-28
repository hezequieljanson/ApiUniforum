using System.ComponentModel.DataAnnotations;

namespace UniversityForumAPI.DTOs.TopicDTOs
{
    public class UpdateTopicDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
