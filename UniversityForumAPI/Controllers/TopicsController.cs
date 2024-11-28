using Microsoft.AspNetCore.Mvc;
using UniversityForumAPI.Services;
using UniversityForumAPI.DTOs.TopicDTOs;

namespace UniversityForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicsController(TopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDto>> GetTopic(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null) return NotFound();

            return Ok(topic);
        }

        [HttpGet("group/{groupId}")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopicsByGroup(int groupId)
        {
            var topics = await _topicService.GetTopicsByGroupIdAsync(groupId);
            return Ok(topics);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTopic([FromBody] CreateTopicDto createTopicDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTopic = await _topicService.CreateTopicAsync(createTopicDto);
            return CreatedAtAction(nameof(GetTopic), new { id = createdTopic.Id }, createdTopic);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTopic(int id, [FromBody] UpdateTopicDto updateTopicDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTopic = await _topicService.UpdateTopicAsync(id, updateTopicDto);
            if (updatedTopic == null)
                return NotFound();

            return Ok(updatedTopic);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopic(int id)
        {
            var deleted = await _topicService.DeleteTopicAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetRecentTopics(int limit = 5)
        {
            // Chama o serviço para pegar os tópicos mais recentes
            var topics = await _topicService.GetRecentTopicsAsync(limit);
            return Ok(topics);
        }
    }
}
