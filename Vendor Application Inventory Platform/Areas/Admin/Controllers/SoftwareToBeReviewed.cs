using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareToBeReviewed : Controller
    {

        private readonly AppDbContext _appDbContext;
        public SoftwareToBeReviewed(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("Getting number of software to be reviewed");
            var softwareToBeReviewed = _appDbContext.softwareToBeRevieweds.ToList();

            int numberOfSoftwareToBeReviewed = softwareToBeReviewed.Count;
            System.Diagnostics.Debug.WriteLine("numberOfSoftwareToBeReviewedd");
            return Ok(numberOfSoftwareToBeReviewed);
        }
    }
}