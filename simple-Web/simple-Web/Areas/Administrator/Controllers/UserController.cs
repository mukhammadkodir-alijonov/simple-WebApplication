using Microsoft.AspNetCore.Mvc;
using simple_Web.Service.Common.Utils;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces;

namespace simple_Web.Areas.Administrator.Controllers
{
    [Route("adminUsers")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly int pageSize = 20;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet("getall")]
        public async Task<ViewResult> Index(int page = 1)
        {
            var users = await _userService.GetAllUsernameAysnc(new PaginationParams(page, pageSize));
            return View("Index", users);
        }
        [HttpGet("update")]
        public async Task<IActionResult> UpdateRedirectAsync(int userid)
        {
            var user = await _userService.GetAsync(userid);
            user.Id = userid;
            var upuser = new UserUpdateDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status
            };
            ViewBag.userid = userid;
            ViewBag.HomeTitle = "user / Get / Update";
            return View("UserUpdate", upuser);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int userid)
        {
            var res = await _userService.DeleteAsync(userid);
            if (res)
            {
                return RedirectToAction("Index", "users", new { area = "Adminstrator" });
            }
            return RedirectToAction("Index", "users", new { area = "Adminstrator" });
        }
    }

}
