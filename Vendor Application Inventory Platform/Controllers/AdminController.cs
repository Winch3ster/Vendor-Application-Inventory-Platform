using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Data.Services;
using Vendor_Application_Inventory_Platform.Models;
using Microsoft.EntityFrameworkCore;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Controllers
{



    [Authorize(Policy = "AdminPolicy")]
    [Route("[controller]/[action]/")]
  
    public class AdminController : Controller
    {
        public readonly IUserEmployeeServices _service;
        public AdminController(IUserEmployeeServices service)
        {
            _service = service;
        }

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
            return View();
        }

        [Route("~/Admin/Employees")]
        public async Task<IActionResult> Employees() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only


            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list

            return View(allEmployeesData); //pass the data list to the view
        }
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




        //Get request Admin/Employees/id
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            //check if that actor exist in database
            var employeeDetails = await _service.GetByIdAsync(id);

            //If dowsnt exist return message
            if (employeeDetails == null)
            {
                return View("NotFound");
            }
            return View(employeeDetails);

                return Json(new { success = true });
            }


        }


        // GET: /Employees/Update/1
        [HttpGet]
        [ActionName("Update")]
        //[Route("~/Admin/Employee/Update/{id}")]
       
      

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
            {
                return View("NotFound");
            }

            return View(employee);
        }

        // POST: /Products/Update/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        //[Route("~/Admin/Employee/Update/{id}")]
        //[Route("Employee/Update/{id}")]

        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, employee);
                return RedirectToAction("Employees");
            }

            return View(employee);
        }

            return Json(new { success = false });
        }
    }
}