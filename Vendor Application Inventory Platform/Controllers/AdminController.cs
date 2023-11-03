using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        [Route("~/Admin")]
        [Route("~/Admin/Index")]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AddEntries()
        {
            return View();
        }


    }
}
