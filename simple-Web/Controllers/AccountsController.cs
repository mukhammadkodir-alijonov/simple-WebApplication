﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Helpers;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces;
using simple_Web.Service.Services.Common;

namespace simple_Web.Controllers
{
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService accountService)
        {
            this._service = accountService;
        }
        [HttpGet("register")]
        public ViewResult Register() => View("Register");

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(AccountRegisterDto accountRegisterDto)
        {
            if (ModelState.IsValid)
            {

                bool result = await _service.RegisterAsync(accountRegisterDto);
                if (result)
                {
                    return RedirectToAction("login", "accounts", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("Email", "This Email is already Existed.");
                    return View(accountRegisterDto);
                }

            }
            else return Register();
        }
        [HttpGet("login")]
        public ViewResult Login() => View("Login");

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(AccountLoginDto accountLoginDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = await _service.LoginAsync(accountLoginDto);
                    if (token != string.Empty)
                    {
                        HttpContext.Response.Cookies.Append("X-Access-Token", token, new CookieOptions()
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict
                        });
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Your account is blocked, pls contact Admin");
                        return View(accountLoginDto);
                    }
                }
                catch (NotFoundException ex)
                {
                    ModelState.AddModelError("Email", ex.Message);
                    return View(accountLoginDto);
                }
                
            }
            else return Login();
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Append("X-Access-Token", "", new CookieOptions()
            {
                Expires = TimeHelper.GetCurrentServerTime().AddDays(-1)
            });
            return RedirectToAction("login", "accounts", new { area = "" });
        }
    }
}
