// Controllers/CommentsController.cs
using Microsoft.AspNetCore.Mvc;
using UniversityForumAPI.DTOs.CommentDTOs;
using UniversityForumAPI.Models;
using UniversityForumAPI.Services;

namespace UniversityForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentReadDTO>> GetComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();

            return Ok(new CommentReadDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserId = comment.UserId,
                TopicId = comment.TopicId
            });
        }

        [HttpGet("topic/{topicId}")]
        public async Task<ActionResult<IEnumerable<CommentReadDTO>>> GetCommentsByTopic(int topicId)
        {
            var comments = await _commentService.GetCommentsByTopicIdAsync(topicId);
            var commentDtos = comments.Select(c => new CommentReadDTO
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UserId = c.UserId,
                TopicId = c.TopicId
            });

            return Ok(commentDtos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] CommentCreateDTO commentDto)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                TopicId = commentDto.TopicId
            };

            await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComment(int id, [FromBody] CommentCreateDTO commentDto)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();

            comment.Content = commentDto.Content;
            await _commentService.UpdateCommentAsync(comment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
