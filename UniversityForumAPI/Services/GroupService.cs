using UniversityForumAPI.DTOs.GroupDTOs;
using UniversityForumAPI.Models;
using UniversityForumAPI.Repositories.GroupRepository;

namespace UniversityForumAPI.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> CreateGroupAsync(CreateGroupDto dto, int ownerId)
        {
            var group = new Group
            {
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = ownerId,
                Icon = dto.Icon,
                BackgroundImage = dto.BackgroundImage
            };

            return await _groupRepository.CreateAsync(group);
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return await _groupRepository.GetByIdAsync(groupId);
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task UpdateGroupAsync(int groupId, UpdateGroupDto dto)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null) throw new Exception("Group not found.");

            group.Name = dto.Name;
            group.Description = dto.Description;
            group.Icon = dto.Icon;
            group.BackgroundImage = dto.BackgroundImage;

            await _groupRepository.UpdateAsync(group);
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            await _groupRepository.DeleteAsync(groupId);
        }

        public async Task<IEnumerable<Group>> GetGroupsCreatedByUserAsync(int userId)
        {
            return await _groupRepository.GetGroupsByOwnerAsync(userId);
        }

        public async Task<IEnumerable<Group>> GetRecentGroupsAsync()
        {
            // Assume que a interface do repositório tem um método que suporta ordenação
            return await _groupRepository.GetRecentGroupsAsync();
        }

    }
}
