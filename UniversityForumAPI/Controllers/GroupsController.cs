using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityForumAPI.DTOs.GroupDTOs;
using UniversityForumAPI.Services;

namespace UniversityForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _groupService;

        public GroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto dto)
        {
            var ownerId = int.Parse(User.FindFirst("id")?.Value);
            var group = await _groupService.CreateGroupAsync(dto, ownerId);
            return CreatedAtAction(nameof(GetGroupById), new { groupId = group.Id }, group);
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupById(int groupId)
        {
            var group = await _groupService.GetGroupByIdAsync(groupId);
            if (group == null) return NotFound("Group not found.");
            return Ok(group);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpPut("{groupId}")]
        [Authorize]
        public async Task<IActionResult> UpdateGroup(int groupId, [FromBody] UpdateGroupDto dto)
        {
            await _groupService.UpdateGroupAsync(groupId, dto);
            return NoContent();
        }

        [HttpDelete("{groupId}")]
        [Authorize]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            await _groupService.DeleteGroupAsync(groupId);
            return NoContent();
        }

        [HttpGet("created")]
        [Authorize]
        public async Task<IActionResult> GetGroupsCreatedByUser()
        {
            var ownerId = int.Parse(User.FindFirst("id")?.Value); // Obtém o id do usuário autenticado
            var groups = await _groupService.GetGroupsCreatedByUserAsync(ownerId);
            return Ok(groups);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentGroups()
        {
            // Recupera os 20 grupos mais recentes, ordenados pela data de criação (CreatedAt) em ordem decrescente
            var groups = await _groupService.GetRecentGroupsAsync();
            return Ok(groups);
        }
    }
}
