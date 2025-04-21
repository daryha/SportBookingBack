// Services/UserService.cs
using BookingSports.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _userManager.Users.ToListAsync();

        public Task<User?> GetUserByIdAsync(string id) =>
            _userManager.FindByIdAsync(id);

        public async Task<User?> UpdateUserAsync(string id, User updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            user.FirstName = updatedUser.FirstName ?? user.FirstName;
            user.LastName  = updatedUser.LastName  ?? user.LastName;
            user.City      = updatedUser.City      ?? user.City;
            user.Email     = updatedUser.Email     ?? user.Email;
            user.UserName  = updatedUser.Email     ?? user.UserName;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? user : null;
        }

        public async Task<IList<string>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user == null
                ? new List<string>()
                : await _userManager.GetRolesAsync(user);
        }
    }
}
