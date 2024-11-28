﻿namespace UniversityForumAPI.DTOs.CommentDTOs
{
    public class CommentReadDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
    }
}
