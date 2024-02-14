using Microsoft.AspNetCore.Mvc;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.ViewModels;
using System.Security.Principal;

namespace simple_Web.ViewComponents
{
    public class IdentityViewComponent : ViewComponent
    {
        private readonly IIdentityService _identityService;
        public IdentityViewComponent(IIdentityService identity)
        {
            this._identityService = identity;
        }
        public IViewComponentResult Invoke()
        {
            AccountBaseViewModel accountBaseViewModel = new AccountBaseViewModel()
            {
                UserName = _identityService.UserName
            };
            return View(accountBaseViewModel);
        }
    }
}
