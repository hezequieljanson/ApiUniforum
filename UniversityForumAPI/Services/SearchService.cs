using Microsoft.EntityFrameworkCore;
using UniversityForumAPI.Data;
using UniversityForumAPI.DTOs.TopicDTOs;
using System.Linq;
using UniversityForumAPI.DTOs;

namespace UniversityForumAPI.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<TopicDto> topics, List<GroupDTO> groups)> SearchAsync(string query, int page, int pageSize)
        {
            // Pesquisa de tópicos
            var topicsQuery = _context.Topics
                .Include(t => t.Group)  // Inclui o grupo associado ao tópico
                .Include(t => t.User)   // Inclui o usuário associado ao tópico
                .Where(t => t.Title.Contains(query) || t.Content.Contains(query))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Content,
                    CreatedAt = t.CreatedAt,
                    UserId = t.User.Id,
                    UserName = t.User.Name,  // Usando Name em vez de UserName
                    GroupId = t.Group.Id,
                    GroupName = t.Group.Name
                });

            // Pesquisa de grupos
            var groupsQuery = _context.Groups
                .Where(g => g.Name.Contains(query) || g.Description.Contains(query))
                .Skip((page - 1) * pageSize)
            .Take(pageSize)
                .Select(g => new GroupDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    CreatedAt = g.CreatedAt
                });

            var topics = await topicsQuery.ToListAsync();
            var groups = await groupsQuery.ToListAsync();

            return (topics, groups);
        }
    }
}
