using simple_Web.Domain.Comman;
using simple_Web.Domain.Enums;

namespace simple_Web.Domain.Entities
{
    public class Admin : Human
    {
        public Role Role { get; set; } = Role.Admin;
    }
}
