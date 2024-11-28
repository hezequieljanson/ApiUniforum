using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityForumAPI.DTOs;
using UniversityForumAPI.DTOs.UserDTOs;
using UniversityForumAPI.Services;

namespace UniversityForumAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var token = await _authService.RegisterAsync(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = int.Parse(User.FindFirst("id").Value); // Assume que o "id" foi adicionado no token
            var user = await _authService.GetUserProfileAsync(userId);
            if (user == null) return NotFound("User not found.");

            return Ok(new { user.Name, user.Email, user.ProfilePicture });
        }

        [Authorize]
        [HttpPut("profile-update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateDto)
        {
            var userId = int.Parse(User.FindFirst("id").Value);

            try
            {
                await _authService.UpdateUserProfileAsync(userId, updateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            // Busca o usuário pelo ID
            var user = await _authService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }

            return Ok(new
            {
                user.Name,
                user.Email,
                user.ProfilePicture
            });
        }
    }
}
