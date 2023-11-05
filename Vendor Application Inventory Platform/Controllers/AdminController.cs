using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

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

        [HttpGet]
        public IActionResult AddEntries()
        {
            var objCountries = _db.Companies
                .Where(c => c.CompanyID == 2)
                .SelectMany(c => c.Countries)
                .Select(country => country.CountryName).ToList();
            
            return View(objCountries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCountry(string[] countryNames)
        {
            if (ModelState.IsValid)
            {
                var company = _db.Companies.Find(2);

                foreach (var countryName in countryNames)
                {
                    if (company != null)
                    {
                        var countryExistsInDatabase = _db.Companies
                            .SelectMany(c => c.Countries)
                            .Select(country => country.CountryName)
                            .ToList();
                        
                        if (!countryExistsInDatabase.Contains(countryName))
                        {
                            var country = new Country
                            {
                                CountryName = countryName,
                                Companies = new List<Company> { company }
                            };

                            _db.Countries.Add(country);
                            _db.SaveChanges();
                        }
                        else
                        {
                            var existingCountry = _db.Countries.FirstOrDefault(c => c.CountryName == countryName);
                            
                        }
                    }
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}