using UniversityForumAPI.Models;
using Microsoft.EntityFrameworkCore;
using UniversityForumAPI.Data;

namespace UniversityForumAPI.Repositories.GroupRepository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> GetByIdAsync(int groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int groupId)
        {
            var group = await GetByIdAsync(groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Group>> GetGroupsByOwnerAsync(int ownerId)
        {
            return await _context.Groups
                .Where(g => g.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetRecentGroupsAsync()
        {
            return await _context.Groups
                                 .OrderByDescending(g => g.CreatedAt) // Ordena pela data de criação
                                 .Take(35) // Limita a 20 grupos
                                 .ToListAsync();
        }
    }
}
