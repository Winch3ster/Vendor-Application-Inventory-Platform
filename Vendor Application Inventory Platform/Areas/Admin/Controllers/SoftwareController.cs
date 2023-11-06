using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    public class SoftwareController : Controller
    {
        public readonly ISoftwareServices _services;

        public SoftwareController(ISoftwareServices s)
        {
            _services = s;
        }


        public IActionResult Index()
        {
            return View();
        }





    }
}
