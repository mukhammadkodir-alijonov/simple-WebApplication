using simple_Web.Domain.Comman;

namespace simple_Web.Domain.Entities
{
    public class Human : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
