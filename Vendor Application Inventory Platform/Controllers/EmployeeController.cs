using Microsoft.AspNetCore.Mvc;

namespace Vendor_Application_Inventory_Platform.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Edit()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> RandomThings()
        {
            return View();
        }



    }
}
