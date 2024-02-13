using simple_Web.Service.Interfaces.Common;

namespace simple_Web.Service.Services.Common
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _accessor;
        public IdentityService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
        public int? Id
        {
            get
            {
                var res = _accessor.HttpContext!.User.FindFirst("Id");
                return res is not null ? int.Parse(res.Value) : null;
            }
        }
        public string UserName
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("FirstName");
                return (result is null) ? String.Empty : result.Value;
            }
        }
    }

}
