// Controllers/UserController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _svc;

        public UserController(IUserService svc)
        {
            _svc = svc;
        }

        // GET api/users/me
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var user  = await _svc.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await _svc.GetUserRolesAsync(userId);
            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.City,
                Roles = roles
            });
        }

        // PUT api/users/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] User model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var updated = await _svc.UpdateUserAsync(userId, model);
            if (updated == null) return BadRequest(new { message = "Не удалось обновить профиль" });

            return Ok(updated);
        }

        // GET api/users (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await _svc.GetAllUsersAsync());
    }
}
