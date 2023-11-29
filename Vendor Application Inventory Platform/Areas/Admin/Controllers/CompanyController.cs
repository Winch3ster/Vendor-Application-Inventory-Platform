using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers;

[Authorize(Policy = "Authentication")]
[Authorize(Policy = "AdminOrCompanyPolicy")]
[Area("Admin")]
[Route("[controller]/[action]/")]
public class CompanyController: Controller
{
    
        private readonly ICompanyServices _companyService;
        private readonly IChangeLogService _changeLogService;
        private readonly EmailService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public CompanyController(ICompanyServices companyService, IChangeLogService changeLogService, EmailService emailService, IHubContext<NotificationHub> hubContext)
        {
            _companyService = companyService;
            _changeLogService = changeLogService;   
            _emailService = emailService;
            _hubContext = hubContext;
        }


        [HttpGet]
        public IActionResult CreateCompany(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                int companyId = id ?? 0;
                Company? company = _companyService.FindCompanyById(companyId);
                
                if (company == null)
                {
                    return NotFound();
                }

                return View(company);

            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult CreateCompany([FromBody] CreateCompanyField createCompanyField, int? id)
        {
            if (id != null && id != 0 && id!= -1)
            {
                if (ModelState.IsValid)
                {
                    int companyId = id ?? 0;
                    Company? company = _companyService.UpdateCompany(companyId, createCompanyField);
                    if (company == null)
                    {
                        return NotFound();
                    }
                    return Json(new { success = true, companyID = company!.CompanyID });
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .FirstOrDefault();
                    return Json(new { success = false, errors = errors });
                }
            }
            
            if (ModelState.IsValid)
            {
                Company company = _companyService.CreateNewCompany(createCompanyField);

            ///////////////////////////////////////////////////////////




            _changeLogService.AddChange(company.CompanyName, Actions.added);
            _hubContext.Clients.All.SendAsync("notification", "update");

            _emailService.SendEmail(User.FindFirstValue(ClaimTypes.NameIdentifier), "Company Added", "Company", company.CompanyName, "added");



            ////////////////////////////////////////////////////
            return Json(new { success = true, companyID = company.CompanyID });
            }
            else
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();
                return Json(new { success = false, errors = errors });
            }
        }
    
        [HttpGet]
        public IActionResult CreateCompanyDetails(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            int companyId = id?? 0;

            _companyService.FindCompanyById(companyId);
                
            Company? company = _companyService.FindCompanyById(companyId);

            if (company == null)
            {
                return NotFound();
            }
            
            List<string> countriesNames = _companyService.CountryNamesByCompany(companyId).ToList();
            var countryViewModels = new List<CreateCompanyDetailsViewModel.CountryViewModel>();

            foreach (var countryName in countriesNames)
            {
                var cities = _companyService.ListCities(companyId, countryName).ToList();

                var cityViewModels = new List<CreateCompanyDetailsViewModel.CityViewModel>();

                foreach (var city in cities)
                {
                    var address = _companyService.GetAddress(city.CityID);
                    var contact = _companyService.GetContact(city.CityID);

                    var cityViewModel = new CreateCompanyDetailsViewModel.CityViewModel
                    {
                        CityName = city.CityName,
                        Address = new CreateCompanyDetailsViewModel.AddressViewModel
                        {
                            AddressLine1 = address?.AddressLine1,
                            AddressLine2 = address?.AddressLine2,
                            PostCode = address?.PostCode,
                            State = address?.State,
                        },
                        Contact = new CreateCompanyDetailsViewModel.ContactViewModel
                        {
                            ContactNumber = contact?.Number,
                        },
                    };

                    cityViewModels.Add(cityViewModel);
                }

                var countryViewModel = new CreateCompanyDetailsViewModel.CountryViewModel
                {
                    CountryName = countryName,
                    Cities = cityViewModels,
                };
                countryViewModels.Add(countryViewModel);
            }

            var viewModel = new CreateCompanyDetailsViewModel()
            {
                companyId = companyId,
                Countries = countryViewModels,
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCountry(string[] countryNames, int? id)
        {
            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            int companyId = id ?? 0;
            List<string> updatedCountryNames = new List<string>();
            if (ModelState.IsValid)
            {
                var company = _companyService.FindCompanyById(companyId);
                var thisCompanyCountriesName = _companyService.CountryNamesByCompany(companyId).ToArray();

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
                            _companyService.CreateNewCountry(countryName, company);
                        }
                        
                        
                        if (thisCompanyCountriesName != null)
                        {
                            var deleteCountries = thisCompanyCountriesName.Except(updatedCountryNames.ToArray()).ToArray();
                            if (deleteCountries.Any())
                            {
                                foreach (var country in deleteCountries)
                                {
                                    _companyService.DeleteCountry(country, companyId);
                                }
                            }

                        }
                    }
                }
                
                return Json(new { success = true , countryNames = _companyService.CountryNamesByCompany(companyId).ToArray()});
                
            }
            return Json(new { success = false });
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCities([FromBody] CityField cityField, int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            int companyId = id ?? 0;
            if (ModelState.IsValid)
            {
                City? city = null;
                bool hasAddress = cityField.HasAddress;
                bool hasContact = cityField.HasContact;
                string country = cityField.Country;
                string cityName = Regex.Replace(cityField.CityName.Trim(), @"\s+", " ").ToUpper();
                
                List<string> citiesNames = _companyService.ListCities(companyId, cityField.Country).Select(c=>c.CityName).ToList();
                
                Country countryObj = _companyService.RetrieveCountry(country, companyId);
                
                if (!citiesNames.Contains(cityName))
                {
                    
                    city = _companyService.CreateNewCity(cityName, countryObj);
                    
                }
                else
                {
                    city = _companyService.FindCity(companyId, countryObj.CountryName, cityName);
                }
                
                if (hasContact)
                {
                    string? contactNumber = cityField.ContactNumber;
                    if (contactNumber != null) 
                        _companyService.CreateNewContact(contactNumber, city);
                }
                else
                {
                    _companyService.DeleteContact(city);
                }

                    
                if (hasAddress)
                {
                    string? address1 = cityField.Address1;
                    string? address2 = cityField.Address2;
                    string? postcode = cityField.Postcode;
                    string? state = cityField.State;

                    if (address1 != null && address2 != null && postcode != null)
                    {
                        _companyService.CreateNewAddress(address1, address2, postcode, state, city);
                    }
                }
                else
                {
                    _companyService.DeleteAddress(city);
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
        public IActionResult DeleteCities([FromBody] DeleteCityField deleteCityField, int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            int companyId = id ?? 0;
            if (deleteCityField.deleteCities != null)
                foreach (var deleteCity in deleteCityField.deleteCities)
                {
                    City? city = _companyService.FindCity(companyId, deleteCity.Country.ToUpper(), deleteCity.City.ToUpper());

                    if (city != null)
                    {
                        _companyService.DeleteCity(city);
                    }

                    
                }

            return Json(new { success = true });
        }

        [Route("~/Company")]
        [Route("~/Company/Index")]
        public IActionResult Index()
        {
            List<Company?> companies = _companyService.ListCompanies();
            return View(companies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult DeleteCompany(int id)
        {
            if (id != null && id != 0)
            {
                _changeLogService.AddChangeDeleteCompanyById(id, Actions.deleted);
                _companyService.DeleteCompany(id);
            }



        ///////////////////////////////////////////////////////////


        _hubContext.Clients.All.SendAsync("notification", "update");



        _emailService.SendEmail(User.FindFirstValue(ClaimTypes.NameIdentifier), "Company Removed", "Company", "A company", "removed");

    

        ////////////////////////////////////////////////////
            return RedirectToAction("Index");
    }
}