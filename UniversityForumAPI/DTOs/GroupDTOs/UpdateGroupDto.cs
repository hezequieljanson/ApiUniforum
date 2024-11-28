namespace UniversityForumAPI.DTOs.GroupDTOs
{
    public class UpdateGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Icon { get; set; }
        public string? BackgroundImage { get; set; }
    }
}
