using simple_Web.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace simple_Web.Service.Dtos
{
    public class UserUpdateDto
    {
        [Required, MaxLength(30), MinLength(3)]
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public StatusType Status { get; set; }
    }
}
