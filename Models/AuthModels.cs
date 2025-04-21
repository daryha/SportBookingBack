// Models/AuthModels.cs
using System.ComponentModel.DataAnnotations;

namespace BookingSports.Models
{
    public class RegisterModel
    {
        [Required]               public string FirstName { get; set; } = null!;
        [Required]               public string LastName  { get; set; } = null!;
        [Required]               public string City      { get; set; } = null!;
        [Required, EmailAddress] public string Email     { get; set; } = null!;
        [Required, MinLength(6)] public string Password  { get; set; } = null!;
        public string Role        { get; set; } = "User";
    }

    public class LoginModel
    {
        [Required, EmailAddress] public string Email    { get; set; } = null!;
        [Required]               public string Password { get; set; } = null!;
    }

    public class UpdateUserModel
    {
        [Required] public string FirstName { get; set; } = null!;
        [Required] public string LastName  { get; set; } = null!;
        [Required] public string City      { get; set; } = null!;
    }
}
