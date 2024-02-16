using System.ComponentModel.DataAnnotations;

namespace simple_Web.Service.Dtos
{
    public class AccountRegisterDto : AccountLoginDto
    {

        [Required(ErrorMessage = "Enter a UserName!")]
        public string UserName { get; set; } = String.Empty;
    }
}
