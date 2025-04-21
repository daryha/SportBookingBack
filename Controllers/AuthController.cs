// Controllers/AuthController.cs

using BookingSports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookingSports.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName  = model.Email,
                Email     = model.Email,
                FirstName = model.FirstName,
                LastName  = model.LastName,
                City      = model.City
            };

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest(new { message = "Пользователь с таким email уже существует." });

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Присваиваем роль (по умолчанию — User)
            var role = string.IsNullOrEmpty(model.Role) ? "User" : model.Role;
            if (new[] { "Admin", "Coach", "SportFacility", "User" }.Contains(role))
                await _userManager.AddToRoleAsync(user, role);
            else
                return BadRequest(new { message = "Неверная роль!" });

            return Ok(new { message = "Регистрация прошла успешно!" });
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Неверный логин или пароль.");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
                return Unauthorized("Неверный логин или пароль.");

            // Генерация JWT
            var token = GenerateJwtToken(user);

            // Получаем список ролей пользователя
            var roles = await _userManager.GetRolesAsync(user);

            // Возвращаем токен + данные пользователя + роли
            return Ok(new
            {
                token,
                firstName = user.FirstName,
                lastName  = user.LastName,
                city      = user.City,
                email     = user.Email,
                roles     // <-- вот здесь
            });
        }

        // GET api/auth/me
        [HttpGet("me"), Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.City,
                roles     // <-- и здесь
            });
        }

        // PUT api/auth/me
        [HttpPut("me"), Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] User model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName ?? user.FirstName;
            user.LastName  = model.LastName  ?? user.LastName;
            user.City      = model.City      ?? user.City;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Профиль обновлён!" });
        }

        // Вспомогательный метод генерации JWT
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:            _configuration["Jwt:Issuer"],
                audience:          _configuration["Jwt:Audience"],
                claims:            claims,
                expires:           DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
