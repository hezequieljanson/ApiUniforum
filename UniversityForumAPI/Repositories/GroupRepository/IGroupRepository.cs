using UniversityForumAPI.Models;

namespace UniversityForumAPI.Repositories.GroupRepository
{
    public interface IGroupRepository
    {
        Task<Group> CreateAsync(Group group);
        Task<Group> GetByIdAsync(int groupId);
        Task<IEnumerable<Group>> GetAllAsync();
        Task UpdateAsync(Group group);
        Task DeleteAsync(int groupId);
        Task<IEnumerable<Group>> GetGroupsByOwnerAsync(int ownerId);
        Task<IEnumerable<Group>> GetRecentGroupsAsync();
    }
}
