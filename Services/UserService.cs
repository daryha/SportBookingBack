using BookingSports.Data;
using BookingSports.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingSports.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> UpdateUserAsync(string id, User updatedUser);
        Task<IList<string>> GetUserRolesAsync(string id);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> UpdateUserAsync(string id, User updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            user.FirstName = updatedUser.FirstName ?? user.FirstName;
            user.LastName = updatedUser.LastName ?? user.LastName;
            user.City = updatedUser.City ?? user.City;
            user.Email = updatedUser.Email ?? user.Email;
            user.UserName = updatedUser.Email ?? user.UserName;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? user : null;
        }

        public async Task<IList<string>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }
    }
}
