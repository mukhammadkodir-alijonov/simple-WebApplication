using Microsoft.AspNetCore.Mvc;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Interfaces;
using simple_Web.Service.ViewModels;

namespace simple_Web.Controllers
{
    [Route("admins")]
    //[Authorize(Roles = "SuperAdmin")]
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IIdentityService _identityService;

        public AdminsController(IAdminService adminService, IIdentityService identityService)
        {
            this._adminService = adminService;
            this._identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            List<AdminViewModel> admins;
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.AdminSearch = search;
                admins = await _adminService.GetAllAsync(search);
            }
            else
            {
                admins = await _adminService.GetAllAsync();
            }

            return View(admins);
        }


        [HttpGet("update(GetByIdAsync)")]
        public async Task<ViewResult> UpdateAsync(int adminId)
        {
            var admin = await _adminService.GetByIdAsync(adminId);
            var adminUpdate = new AdminUpdateDto()
            {
                UserName = admin.UserName,
            };

            return View("../Admins/Update", adminUpdate);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] AdminUpdateDto adminUpdateDto, int adminId)
        {
            var admin = await _adminService.UpdateAsync(adminId, adminUpdateDto);
            if (admin) return await UpdateAsync(adminId);
            else return await UpdateAsync(adminId);
        }

        [HttpGet("delete")]
        public async Task<ViewResult> DeleteAsync(int adminId)
        {
            var admin = await _adminService.GetByIdAsync(adminId);
            if (admin != null) return View("Delete", admin);
            else return View("admins");
        }

        [HttpDelete("deleteAdmin")]
        public async Task<IActionResult> DeleteAdminAsync(int adminId)
        {
            var admin = await _adminService.DeleteAsync(adminId);
            if (admin) return RedirectToAction("index", "admins", new { area = "" });
            else return View();
        }
    }
}