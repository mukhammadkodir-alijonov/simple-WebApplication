using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple_Web.Domain.Entities;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Utils;
using simple_Web.Service.Interfaces;
using simple_Web.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simple_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private const int PageSize =20;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _userService.GetAllAysnc(new PaginationParams(page, PageSize));
            return View("Index", result);
        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(List<int> ids)
        {
            var result = await ExecuteActionAsync(() => _userService.DeleteAsync(ids));
            return result ? RedirectToAction("Index", "Home") : NotFound();
        }

        [HttpPost("block")]
        [Authorize]
        public async Task<IActionResult> BlockAsync(List<int> ids)
        {
            var result = await ExecuteActionAsync(() => _userService.BlockAsync(ids));
            return result ? RedirectToAction("Index", "Home") : NotFound();
        }

        [HttpPost("unblock")]
        [Authorize]
        public async Task<IActionResult> UnBlockAsync(List<int> ids)
        {
            var result = await ExecuteActionAsync(() => _userService.ActiveAsync(ids));
            return result ? RedirectToAction("Index", "Home") : NotFound();
        }

        private async Task<bool> ExecuteActionAsync(Func<Task<bool>> action)
        {
            try
            {
                return await action();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
