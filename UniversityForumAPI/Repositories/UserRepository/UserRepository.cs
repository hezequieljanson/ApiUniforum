using Microsoft.EntityFrameworkCore;
using UniversityForumAPI.Data;
using UniversityForumAPI.Models;

namespace UniversityForumAPI.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            // Supondo que você tenha um modelo de usuário chamado "User"
            return await _context.Users
                                 .Where(u => u.Id == userId)
                                 .FirstOrDefaultAsync();
        }

    }
}
