using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Data.Services;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Controllers;

[Route("[controller]/[action]")]
public class AccessController : Controller
{
    
    private readonly IAccessServices _service; //Inject the service of actors in here

    public AccessController(IAccessServices service)
    {
        _service = service;
    }
    
    [Route("~/")]
    [Route("~/Access/Login")]
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity!.IsAuthenticated)
        {
            Claim? isAdminClaim = claimUser.Claims.FirstOrDefault(c => c is { Type: "isAdmin", Value: "true" });
            
            if (isAdminClaim != null)
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Software", new {area = "User"});
            }
        }

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(VMLogin modelLogin)
    {
        var user = _service.FindUserWithEmail(modelLogin.email);
        if (user!=null)
        {
            bool comparePassword = _service.ComparePasswordsWithBCrypt(modelLogin.password, user.Password);
            if (comparePassword)
            {
                List<Claim> claims = new List<Claim>()
                        {
                           
        
                            new Claim(ClaimTypes.NameIdentifier, modelLogin.email),
                            new Claim("isAdmin", user.IsAdmin ? "true" : "false"),
                            new Claim("softwareAccess", user.softwareAccess? "true" : "false"),
                            new Claim("companyAccess", user.companyAccess? "true" : "false"),
                            new Claim("accountAccess", user.accountAccess? "true" : "false"),
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

                        if (user.IsAdmin)
                        {
                            return RedirectToAction("Index", "Employee", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Software", new { area = "User" });
                        }
            }
        }
        



        ViewData["ValidateMessage"] = "User not found";
        return View();
    }
    
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Access");
    }
    
    
    
}