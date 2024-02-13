using simple_Web.Domain.Entities;
using simple_Web.Domain.Enums;

namespace simple_Web.Service.ViewModels
{
    public class AdminViewModel
    {
        public long Id { get; set; }

        public string UserName { get; set; } = String.Empty;

        public Role Role { get; set; } = Role.Admin;

        public DateTime CreatedAt { get; set; } = default!;

        public static implicit operator AdminViewModel(Admin model)
        {
            return new AdminViewModel()
            {
                Id = model.Id,
                UserName = model.UserName,
                CreatedAt = model.RegistrationTime
            };
        }
    }
}
