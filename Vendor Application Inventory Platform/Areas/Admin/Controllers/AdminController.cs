using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Data.Services;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{



    [Authorize(Policy = "AdminPolicy")]
    [Route("[controller]/[action]/")]
  
    public class AdminController : Controller
    {
        private readonly IUserEmployeeServices _service;
        private readonly IAdminServices _adminService;
        public AdminController(IUserEmployeeServices service, IAdminServices adminService)
        {
            _service = service;
            _adminService = adminService;
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
        
        [Route("~/Admin/Employees")]
        public async Task<IActionResult> Employees() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only
        
        
            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list
        
            return View(allEmployeesData); //pass the data list to the view
        }

        [HttpGet]
        public IActionResult AddEntries()
        {
            var objCountries = _adminService.CountryNamesByCompany(2).ToList();
            
            return View(objCountries);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCountry(string[] countryNames)
        {
            List<string> updatedCountryNames = new List<string>();
            if (ModelState.IsValid)
            {
                var company = _adminService.FindCompanyById(2);
                var thisCompanyCountriesName = _adminService.CountryNamesByCompany(2).ToArray();

                foreach (var countryName in countryNames)
                {
                    var trimmedCountryName = Regex.Replace(countryName.Trim(), @"\s+", " ");

                    var finalCountryName = trimmedCountryName.ToUpper();
                    
                    updatedCountryNames.Add(finalCountryName);
                    
                }
                
                bool hasDuplicates = updatedCountryNames.Count() != updatedCountryNames.Distinct().Count();

                if (hasDuplicates)
                {
                    return Json(new { message = "Duplicate Country Names" });
                }

                foreach (var countryName in updatedCountryNames)
                {
                    if (company != null)
                    {
                        var countryExistsInDatabase = _adminService.CountryNamesInDb();
                        
                        if (!countryExistsInDatabase.Contains(countryName))
                        {
                            _adminService.CreateNewCountry(countryName, company);
                        }
                        else
                        {
                            var existingCountry = _adminService.RetrieveCountry(countryName);
                            
                            _adminService.ConnectCountryToCompany(company, existingCountry);
                            
                        }
                        
                        if (thisCompanyCountriesName != null)
                        {
                            var deleteCountries = thisCompanyCountriesName.Except(updatedCountryNames.ToArray()).ToArray();
                            if (deleteCountries.Any())
                            {
                                foreach (var country in deleteCountries)
                                {
                                    _adminService.DeleteCountry(country, 2);
                                }
                            }

                        }
                    }
                }
                
                return Json(new { success = true , countryNames = _adminService.CountryNamesByCompany(2).ToArray()});
                
            }
            return Json(new { success = false });
        }
        
        
        [HttpPost]
        public IActionResult CreateCities([FromBody] CityField cityField)
        {
            if (ModelState.IsValid)
            {
                bool hasAddress = cityField.HasAddress;
                bool hasContact = cityField.HasContact;
                string country = cityField.Country;
                string cityName = cityField.CityName;

                if (hasAddress)
                {
                    string? address1 = cityField.Address1;
                    string? address2 = cityField.Address2;
                    string? postcode = cityField.Postcode;
                }

                if (hasContact)
                {
                    string? contactNumber = cityField.ContactNumber;
                }

                Country countryObj = _adminService.RetrieveCountry(country);
                _adminService.CreateNewCity(cityName, countryObj);

            }
            else
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();

                return Json(new { errors = errors, country = cityField.Country, city = cityField.CityName});
            }
            return Json("ok");
        }
        

        //Get request Admin/Employees/id
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            //check if that actor exist in database
            var employeeDetails = await _service.GetByIdAsync(id);
        
            //If doesnt exist return message
            if (employeeDetails == null)
            {
                // return View("NotFound");
            }
            return View(employeeDetails);
        
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
                // return View("NotFound");
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

    }
}