using System.ComponentModel.DataAnnotations;

namespace ItsLib.WebApi.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
