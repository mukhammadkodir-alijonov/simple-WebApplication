using simple_Web.Domain.Enums;

namespace simple_Web.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role MyProperty { get; set; } = Role.User;
        public DateTime LastLogin { get; set; }
        public DateTime RegistrationTime { get; set; }
        public StatusType Status { get; set; } = StatusType.Active;
    }
}
