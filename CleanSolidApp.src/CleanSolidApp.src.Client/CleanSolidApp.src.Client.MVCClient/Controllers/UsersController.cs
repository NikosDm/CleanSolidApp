using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanSolidApp.src.Client.MVCClient.Controllers;

[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IAuthService _authService;

    public UsersController(IAuthService authService)
    {
        this._authService = authService;
    }

    public IActionResult Login(string returnUrl = null)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authService.Authenticate(login.Email, login.Password);
            if (isLoggedIn)
                return LocalRedirect(returnUrl);
        }
        ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
        return View(login);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registration)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var isCreated = await _authService.Register(registration);
            if (isCreated)
                return LocalRedirect(returnUrl);
        }
        
        ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
        return View(registration);
    }

    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl)
    {
        returnUrl ??= Url.Content("~/");
        await _authService.Logout();
        return LocalRedirect(returnUrl);
    }
}