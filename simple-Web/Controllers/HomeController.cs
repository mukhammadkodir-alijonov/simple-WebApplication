using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple_Web.Domain.Entities;
using simple_Web.Models;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Helpers;
using simple_Web.Service.Common.Utils;
using simple_Web.Service.Interfaces;
using simple_Web.Service.Services.Common;
using System.Diagnostics;

namespace simple_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private const int PageSize = 40;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IUserService userService,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userService = userService;
            _contextAccessor = httpContextAccessor;
        }
        [Authorize]
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.UserName = _contextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            var result = await _userService.GetAllAysnc(new PaginationParams(page, PageSize));
            return View("Index", result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
