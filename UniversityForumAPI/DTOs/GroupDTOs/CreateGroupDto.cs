namespace UniversityForumAPI.DTOs.GroupDTOs
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Icon { get; set; }
        public string? BackgroundImage { get; set; }
    }
}
