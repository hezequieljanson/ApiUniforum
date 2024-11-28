namespace UniversityForumAPI.DTOs.UserDTOs
{
    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; } // Opcional para quando a senha não for alterada
    }
}
