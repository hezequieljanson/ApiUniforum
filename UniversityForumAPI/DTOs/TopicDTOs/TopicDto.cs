namespace UniversityForumAPI.DTOs.TopicDTOs
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Simplified User and Group information
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
