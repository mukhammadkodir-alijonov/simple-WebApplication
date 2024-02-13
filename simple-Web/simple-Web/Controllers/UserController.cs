using Microsoft.AspNetCore.Mvc;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Utils;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Interfaces;
using simple_Web.Service.ViewModels;

namespace simple_Web.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IIdentityService _identityService;
        private readonly int _pageSize = 2;

        public UserController(IUserService userService, IAccountService accountService, IIdentityService identityService)
        {
            this._userService = userService;
            this._accountService = accountService;
            this._identityService = identityService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetAllUsernameAsync(int page = 1)
        {
            return await HandleExceptionAsync(async () =>
            {
                var users = await _userService.GetAllUsernameAysnc(new PaginationParams(page, _pageSize));
                return Ok(users);
            });
        }
        
        [HttpGet("userId")]
        public async Task<ViewResult> Get(int userId)
        {
            var user = await _userService.GetAsync(userId);
            ViewBag.UserId = userId;
            ViewBag.HomeTitle = "My account";
            var userView = new UserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status
            };
            return View("../Users/Index", userView);
        }

        [HttpGet("update")]
        public async Task<ViewResult> Update()
        {
            var userId = _identityService.Id!.Value;
            var user = await _userService.GetAsync(userId);
            ViewBag.userId = userId;
            ViewBag.HomeTitle = "User update";
            var userUpdate = new UserUpdateDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status
            };
            return View("../Users/Update", userUpdate);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto userUpdateDto, int userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.UpdateAsync(userId, userUpdateDto);
                if (user) return RedirectToAction("Index", "Home", new { area = "" });
                else return RedirectToAction("Update", "Users", new { area = "" });
            }
            else return RedirectToAction("Update", "Users", new { area = "" });
        }
        
        private async Task<IActionResult> HandleExceptionAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
