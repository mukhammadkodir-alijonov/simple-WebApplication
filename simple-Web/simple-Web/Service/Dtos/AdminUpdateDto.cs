using Microsoft.AspNetCore.Mvc;
using simple_Web.Domain.Comman;
using simple_Web.Domain.Entities;
using simple_Web.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace simple_Web.Service.Dtos
{
    public class AdminUpdateDto : BaseEntity
    {
        [Required(ErrorMessage = "UserName Required")]
        public string UserName { get; set; } = String.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public static implicit operator Admin(AdminUpdateDto dto)
        {
            return new Admin()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Role = dto.Role
            };
        }
    }
}
