using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;

namespace Vendor_Application_Inventory_Platform.Areas.User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
      
        public readonly AppDbContext _dbContext;
        public NotificationController(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetNotifications()
        {
            var data = _dbContext.changeLogs.ToList();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }


    }
}
