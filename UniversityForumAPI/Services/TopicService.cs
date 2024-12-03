using UniversityForumAPI.Models;
using UniversityForumAPI.Repositories.TopicRepository;
using UniversityForumAPI.DTOs.TopicDTOs;
using Microsoft.EntityFrameworkCore;

namespace UniversityForumAPI.Services
{
    public class TopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<TopicDto> GetTopicByIdAsync(int id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            if (topic == null) return null;

            // Mapeamento de Topic para TopicDto
            return new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Content = topic.Content,
                CreatedAt = topic.CreatedAt,
                GroupId = topic.GroupId,
                UserId = topic.UserId
            };
        }

        public async Task<IEnumerable<TopicDto>> GetTopicsByGroupIdAsync(int groupId)
        {
            var topics = await _topicRepository.GetAllByGroupIdAsync(groupId);

            // Mapeamento de Topic para TopicDto
            return topics.Select(topic => new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Content = topic.Content,
                CreatedAt = topic.CreatedAt,
                GroupId = topic.GroupId,
                UserId = topic.UserId
            });
        }

        public async Task<TopicDto> CreateTopicAsync(CreateTopicDto createTopicDto)
        {
            // Mapeamento de CreateTopicDto para Topic
            var topic = new Topic
            {
                Title = createTopicDto.Title,
                Content = createTopicDto.Content,
                CreatedAt = DateTime.UtcNow,
                GroupId = createTopicDto.GroupId,
                UserId = createTopicDto.UserId
            };

            await _topicRepository.CreateAsync(topic);

            // Retorna o TopicDto com os dados do tópico recém-criado
            return new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Content = topic.Content,
                CreatedAt = topic.CreatedAt,
                GroupId = topic.GroupId,
                UserId = topic.UserId
            };
        }

        public async Task<TopicDto> UpdateTopicAsync(int id, UpdateTopicDto updateTopicDto)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            if (topic == null) return null;

            // Atualiza os dados do tópico com o conteúdo do UpdateTopicDto
            topic.Title = updateTopicDto.Title;
            topic.Content = updateTopicDto.Content;

            await _topicRepository.UpdateAsync(topic);

            // Retorna o TopicDto atualizado
            return new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Content = topic.Content,
                CreatedAt = topic.CreatedAt,
                GroupId = topic.GroupId,
                UserId = topic.UserId
            };
        }

        public async Task<bool> DeleteTopicAsync(int id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            if (topic == null) return false;

            await _topicRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TopicDto>> GetRecentTopicsAsync(int limit)
        {
            return await _topicRepository.GetRecentTopicsAsync(limit);
        }
    }
}
