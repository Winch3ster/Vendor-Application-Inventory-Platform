using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Controllers;

public class AccessController : Controller
{
    // GET
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Employee");
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(VMLogin modelLogin)
    {
        if (modelLogin is { email: "user@example.com", password: "123" })
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, modelLogin.email),
                new Claim("OtherProperties", "Example Role")
            };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = modelLogin.keepLoggedIn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);
            return RedirectToAction("Index", "Employee");
        }



        ViewData["ValidateMessage"] = "User not found";
        return View();
    }
    
    
}