using UniversityForumAPI.Models;

namespace UniversityForumAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task CreateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
    }
}
