using simple_Web.Domain.Enums;

namespace simple_Web.Domain.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Admin;
        public DateTime LastLogin { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
