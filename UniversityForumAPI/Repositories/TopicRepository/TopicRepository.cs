using Microsoft.EntityFrameworkCore;
using UniversityForumAPI.Data;
using UniversityForumAPI.DTOs.TopicDTOs;
using UniversityForumAPI.Models;

namespace UniversityForumAPI.Repositories.TopicRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task<IEnumerable<Topic>> GetAllByGroupIdAsync(int groupId)
        {
            return await _context.Topics.Where(t => t.GroupId == groupId).ToListAsync();
        }

        public async Task CreateAsync(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TopicDto>> GetRecentTopicsAsync(int limit)
        {
            return await _context.Topics
                .Select(t => new TopicDto {
                Id = t.Id,
                Title = t.Title,
                Content = t.Content,
                CreatedAt = t.CreatedAt,
                UserName = t.User.Name,
                GroupId = t.GroupId,
                GroupName = t.Group.Name,
                UserId = t.UserId
                } )
                .OrderByDescending(t => t.CreatedAt)
                .Take(limit)
                .ToListAsync();  // Retorna os tópicos mais recentes
        }
    }
}
