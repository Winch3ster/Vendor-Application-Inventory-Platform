using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
