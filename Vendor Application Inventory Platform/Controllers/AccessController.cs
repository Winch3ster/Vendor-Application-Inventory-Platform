using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Controllers;

[Route("[controller]/[action]")]
public class AccessController : Controller
{
    private readonly AppDbContext _db;
    public AccessController(AppDbContext db)
    {
        _db = db;
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
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Employee");
            }
        }

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(VMLogin modelLogin)
    {
        var user =  _db.Employees.FirstOrDefault(e => e.Email == modelLogin.email);
        if (user!=null)
        {
            if (BCrypt.Net.BCrypt.Verify(modelLogin.password, user.Password))
            {
                List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, modelLogin.email),
                            new Claim("isAdmin", user.IsAdmin ? "true" : "false")
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
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Employee");
                        }
            }
        }
        



        ViewData["ValidateMessage"] = "User not found";
        return View();
    }
    
    
    
}