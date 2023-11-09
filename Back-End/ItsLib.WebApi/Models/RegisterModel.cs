using System.ComponentModel.DataAnnotations;

namespace ItsLib.WebApi.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nome is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Cognome is required")]
        public string? Surname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Eta is required")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Fiscal code is required")]
        public string? FiscalCode { get; set; }
    }
}
