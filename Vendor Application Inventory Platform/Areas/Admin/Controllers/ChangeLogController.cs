using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeLogController : Controller
    {
        private readonly AppDbContext _context;


        public ChangeLogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChangeLog>> GetNotifications(int userid)
        {
            System.Diagnostics.Debug.WriteLine(userid);
            var user = _context.Employees.FirstOrDefault(e => e.EmployeeID == userid);
            System.Diagnostics.Debug.WriteLine(user);

            var data = _context.changeLogs.Where(cl => cl.time > user.LastRetrieveChangeLog).ToList();
            System.Diagnostics.Debug.Write(data.Count.ToString());
            return Ok(data);
        }
    }
}
