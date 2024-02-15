using simple_Web.Domain.Comman;
using simple_Web.Domain.Enums;

namespace simple_Web.Domain.Entities
{
    public class User: Human
    {
        public StatusType Status { get; set; } = StatusType.Active;
    }
}
