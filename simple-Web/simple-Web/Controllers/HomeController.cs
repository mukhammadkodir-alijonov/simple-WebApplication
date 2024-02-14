using Microsoft.AspNetCore.Mvc;
using simple_Web.Models;
using simple_Web.Service.Common.Utils;
using simple_Web.Service.Interfaces;
using System.Diagnostics;

namespace simple_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private const int PageSize = 2;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
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
