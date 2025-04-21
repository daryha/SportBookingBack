// Services/IUserService.cs
using BookingSports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>>    GetAllUsersAsync();
        Task<User?>                GetUserByIdAsync(string id);
        Task<User?>                UpdateUserAsync(string id, User updatedUser);
        Task<IList<string>>        GetUserRolesAsync(string id);
    }
}
