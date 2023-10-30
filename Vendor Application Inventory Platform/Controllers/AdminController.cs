using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Controllers
{
    public class AdminController : Controller
    {
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
