using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Vendor_Application_Inventory_Platform.Controllers;

[Route("[controller]/[action]")]
public class AccountController:Controller
{
    public IActionResult AccessDenied()
    {
        return View();
    }
}