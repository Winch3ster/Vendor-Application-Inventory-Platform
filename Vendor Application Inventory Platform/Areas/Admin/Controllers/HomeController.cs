using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
