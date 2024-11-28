using System.Security.Cryptography;
using System.Text;
using UniversityForumAPI.DTOs;
using UniversityForumAPI.DTOs.UserDTOs;
using UniversityForumAPI.Helpers;
using UniversityForumAPI.Models;
using UniversityForumAPI.Repositories.UserRepository;

namespace UniversityForumAPI.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // Verifica se o email já está em uso
            if (await _userRepository.GetByEmailAsync(dto.Email) != null)
                throw new Exception("Email já está em uso.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            await _userRepository.CreateAsync(user);
            return _jwtService.GenerateToken(user);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Credenciais inválidas.");

            return _jwtService.GenerateToken(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        public async Task<User> GetUserProfileAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task UpdateUserProfileAsync(int userId, UpdateProfileDto updateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            user.Name = updateDto.Name;
            user.Email = updateDto.Email;

            if (!string.IsNullOrEmpty(updateDto.Password))
            {
                user.PasswordHash = HashPassword(updateDto.Password); // Função que gera o hash da senha
            }

            await _userRepository.UpdateUserAsync(user);
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user;
        }
    }
}
