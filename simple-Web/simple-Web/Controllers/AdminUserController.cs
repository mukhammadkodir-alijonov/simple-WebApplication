using Microsoft.AspNetCore.Mvc;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Services;
using simple_Web.Service.ViewModels;

namespace simple_Web.Controllers
{
    [Route("adminUsers")]
    public class AdminUserController : Controller
    {
        private readonly IAdminUserService _adminUserService;
        private readonly IIdentityService _identityService;
        public AdminUserController(IAdminUserService _adminUserService, IIdentityService _identityService)
        {
            this._adminUserService = _adminUserService;
            this._identityService = _identityService;
        }
        [HttpGet]

        public async Task<IActionResult> Index(string search)
        {
            List<UserViewModel> admins;
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.AdminSearch = search;
                admins = await _adminUserService.GetAllAsync(search);
            }
            else
            {
                admins = await _adminUserService.GetAllAsync();
            }

            return View(admins);
        }
        [HttpGet("update(GetByIdAsync)")]
        public async Task<ViewResult> UpdateAsync(int adminId)
        {
            var admin = await _adminUserService.GetByIdAsync(adminId);
            var adminUpdate = new AdminUpdateDto()
            {
                UserName = admin.UserName,
            };

            return View("../Admins/Update", adminUpdate);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto userUpdateDto, int adminId)
        {
            var admin = await _adminUserService.UpdateAsync(adminId, userUpdateDto);
            if (admin) return await UpdateAsync(adminId);
            else return await UpdateAsync(adminId);
        }

        [HttpGet("delete")]
        public async Task<ViewResult> DeleteAsync(int adminId)
        {
            var admin = await _adminUserService.GetByIdAsync(adminId);
            if (admin != null) return View("Delete", admin);
            else return View("admins");
        }

        [HttpDelete("deleteAdmin")]
        public async Task<IActionResult> DeleteAdminAsync(int adminId)
        {
            var admin = await _adminUserService.DeleteAsync(adminId);
            if (admin) return RedirectToAction("index", "admins", new { area = "" });
            else return View();
        }
    }
}
