using UniversityForumAPI.Models;

namespace UniversityForumAPI.Repositories.CommentRepository
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetAllByTopicIdAsync(int topicId);
        Task CreateAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
    }
}
