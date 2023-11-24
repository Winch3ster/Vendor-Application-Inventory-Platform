using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CurrentUser : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CurrentUser(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ActionResult<Employee> Index()
        {

            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentlySignedInUser = _appDbContext.Employees.FirstOrDefault(e => e.Email == userEmail);

            return Ok(currentlySignedInUser);
        }


        
        [HttpPatch]
        public IActionResult UpdateUserLastRetriveChangeLogTime()
        {

            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentlySignedInUser = _appDbContext.Employees.FirstOrDefault(e => e.Email == userEmail);

            currentlySignedInUser.LastRetrieveChangeLog = DateTime.Now;

            _appDbContext.Update(currentlySignedInUser);
            _appDbContext.SaveChanges();

            string message = "User UserLastRetriveChangeLogTime has been updated";

            return Content(message, "text/plain");
        }
        

    }
        
}
