using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [Route("[controller]/[action]/")]
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    public class SoftwareController : Controller
    {
        private readonly ISoftwareServices _services;
        
        public SoftwareController(ISoftwareServices s)
        {
            _services = s;
        }
        
        [Route("~/Software/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/Software")]
        public async Task<IActionResult> CreateSoftware()
        {
            return View();
        }





    }
}
