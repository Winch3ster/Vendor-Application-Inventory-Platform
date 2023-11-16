using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Controllers
{
    [Authorize(Policy = "Authentication")]
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
