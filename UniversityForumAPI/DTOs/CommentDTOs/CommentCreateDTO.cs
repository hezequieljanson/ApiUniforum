using System.ComponentModel.DataAnnotations;

namespace UniversityForumAPI.DTOs.CommentDTOs
{
    public class CommentCreateDTO
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TopicId { get; set; }
    }
}
