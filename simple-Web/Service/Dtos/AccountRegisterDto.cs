using System.ComponentModel.DataAnnotations;

namespace simple_Web.Service.Dtos
{
    public class AccountRegisterDto : AccountLoginDto
    {

        [Required(ErrorMessage = "Enter a name!")]
        public string UserName { get; set; } = String.Empty;
    }
}
