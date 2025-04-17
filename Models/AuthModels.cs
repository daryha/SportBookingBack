using System.ComponentModel.DataAnnotations;
using BookingSports.Models;

namespace BookingSports.Models
{
    public class RegisterModel
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string City { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, MinLength(6)] public string Password { get; set; }
        public string Role { get; set; } // Добавляем поле для роли
    }

    public class LoginModel
    {
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
