using UniversityForumAPI.Models;

namespace UniversityForumAPI.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<Topic> GetByIdAsync(int id);
        Task<IEnumerable<Topic>> GetAllByGroupIdAsync(int groupId);
        Task CreateAsync(Topic topic);
        Task UpdateAsync(Topic topic);
        Task DeleteAsync(int id);
        Task<IEnumerable<Topic>> GetRecentTopicsAsync(int limit);
    }
}
