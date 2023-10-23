using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Controllers
{
    public class SoftwareController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

    }
}
