using System.ComponentModel.DataAnnotations;

namespace simple_Web.Service.Dtos
{
    public class AccountLoginDto
    {
        [Required(ErrorMessage = "Enter an Email!")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Enter a password!")]
        public string Password { get; set; } = String.Empty;
    }
}
