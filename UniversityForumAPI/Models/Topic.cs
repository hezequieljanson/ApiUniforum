namespace UniversityForumAPI.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        // Propriedade de navegação para Comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
