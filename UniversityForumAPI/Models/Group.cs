namespace UniversityForumAPI.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int OwnerId { get; set; }  // ID do usuário que criou o grupo
        public string? Icon { get; set; }  // Caminho ou URL do ícone do grupo
        public string? BackgroundImage { get; set; }  // Caminho ou URL da imagem de fundo
    }
}
