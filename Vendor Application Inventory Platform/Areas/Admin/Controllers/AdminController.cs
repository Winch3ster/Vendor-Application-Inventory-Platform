using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
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
            List<string> countriesNames = _adminService.CountryNamesByCompany(2).ToList();
            var countryViewModels = new List<AddEntriesViewModel.CountryViewModel>();

            foreach (var countryName in countriesNames)
            {
                var cities = _adminService.ListCities(2, countryName).ToList();

                var cityViewModels = new List<AddEntriesViewModel.CityViewModel>();

                foreach (var city in cities)
                {
                    var address = _adminService.GetAddress(city.CityID);
                    var contact = _adminService.GetContact(city.CityID);

                    var cityViewModel = new AddEntriesViewModel.CityViewModel
                    {
                        CityName = city.CityName,
                        Address = new AddEntriesViewModel.AddressViewModel
                        {
                            AddressLine1 = address?.AddressLine1,
                            AddressLine2 = address?.AddressLine2,
                            PostCode = address?.PostCode,
                        },
                        Contact = new AddEntriesViewModel.ContactViewModel
                        {
                            ContactNumber = contact?.Number,
                        },
                    };

                    cityViewModels.Add(cityViewModel);
                }

                var countryViewModel = new AddEntriesViewModel.CountryViewModel
                {
                    CountryName = countryName,
                    Cities = cityViewModels,
                };
                countryViewModels.Add(countryViewModel);
            }

            var viewModel = new AddEntriesViewModel()
            {
                Countries = countryViewModels,
            };

            return View(viewModel);
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
                        if (!thisCompanyCountriesName!.Contains(countryName))
                        {
                            _adminService.CreateNewCountry(countryName, company);
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
        [ValidateAntiForgeryToken]
        public IActionResult CreateCities([FromBody] CityField cityField)
        {
            if (ModelState.IsValid)
            {
                City? city = null;
                bool hasAddress = cityField.HasAddress;
                bool hasContact = cityField.HasContact;
                string country = cityField.Country;
                string cityName = Regex.Replace(cityField.CityName.Trim(), @"\s+", " ").ToUpper();
                
                List<string> citiesNames = _adminService.ListCities(2, cityField.Country).Select(c=>c.CityName).ToList();
                
                Country countryObj = _adminService.RetrieveCountry(country);
                
                if (!citiesNames.Contains(cityName))
                {
                    
                    city = _adminService.CreateNewCity(cityName, countryObj);
                    
                }
                else
                {
                    city = _adminService.FindCity(2, countryObj.CountryName, cityName);
                }
                
                if (hasContact)
                {
                    string? contactNumber = cityField.ContactNumber;
                    if (contactNumber != null) 
                        _adminService.CreateNewContact(contactNumber, city);
                }
                else
                {
                    _adminService.DeleteContact(city);
                }

                    
                if (hasAddress)
                {
                    string? address1 = cityField.Address1;
                    string? address2 = cityField.Address2;
                    string? postcode = cityField.Postcode;

                    if (address1 != null && address2 != null && postcode != null)
                    {
                        _adminService.CreateNewAddress(address1, address2, postcode, city);
                    }
                }
                else
                {
                    _adminService.DeleteAddress(city);
                }
                    
            }
            else
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();

                return Json(new { errors = errors, country = cityField.Country, city = cityField.CityName});
            }
            return Json(new {success=true});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCities([FromBody] DeleteCityField deleteCityField)
        {
            if (deleteCityField.deleteCities != null)
                foreach (var deleteCity in deleteCityField.deleteCities)
                {
                    City? city = _adminService.FindCity(2, deleteCity.Country.ToUpper(), deleteCity.City.ToUpper());

                    if (city != null)
                    {
                        _adminService.DeleteCity(city);
                    }

                    
                }

            return Json(new { success = true });
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