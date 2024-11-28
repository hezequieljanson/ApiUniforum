using UniversityForumAPI.Models;
using UniversityForumAPI.Repositories.CommentRepository;

namespace UniversityForumAPI.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTopicIdAsync(int topicId)
        {
            return await _commentRepository.GetAllByTopicIdAsync(topicId);
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            await _commentRepository.CreateAsync(comment);
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}
