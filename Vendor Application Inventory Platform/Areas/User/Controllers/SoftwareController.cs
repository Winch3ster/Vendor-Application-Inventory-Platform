using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using Vendor_Application_Inventory_Platform.Areas.User.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;
using Vendor_Application_Inventory_Platform.Views.Employee;

namespace Vendor_Application_Inventory_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class SoftwareController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IExcelGenerationService _excelGenerationService;
        private readonly IPdfGenerationService _pdfGenerationService;
        public SoftwareController(AppDbContext dbContext, IExcelGenerationService excelGenerationService, IPdfGenerationService pdfGenerationService)
        {
            _dbContext = dbContext;
            _excelGenerationService = excelGenerationService;
            _pdfGenerationService = pdfGenerationService;
        }


        public IActionResult Index(string searchString, List<string> filter)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentlySignedInUser = _dbContext.Employees.FirstOrDefault(e => e.Email == userEmail);

            // Get software type list
            var softwareTypeList = _dbContext.SoftwareTypes.ToList();

            // Get all software and recently viewed
            var data = new SoftwareIndexVM()
            {
                returnedSoftwares = _dbContext.Softwares.ToList(),
                userClaims = ((ClaimsIdentity)User.Identity).Claims,
                softwareTypes = softwareTypeList,
                recentlyViewed = _dbContext.user_ViewHistories
                    .Where(u_v => u_v.EmployeeId == currentlySignedInUser.EmployeeID)
                    .OrderByDescending(u_v => u_v.time)
                    .Select(u_v => u_v.U_V_Software)
                    .Distinct().Take(6)
                    .ToList()
            };

            System.Diagnostics.Debug.WriteLine(searchString);

            var softwares = from i in _dbContext.Softwares
                select i;

            // If the search string is not empty, apply the search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                softwares = softwares.Where(s => s.SoftwareName.Contains(searchString) || s.Description.Contains(searchString));
            }

            // If there are filters selected, apply the filter
            if (filter != null && filter.Any())
            {
                softwares = softwares
                    .Where(s => s.Software_Types.Any(st => filter.Contains(st.softwareType.Type)));

            }

            // Update the data with the filtered result
            data = new SoftwareIndexVM()
            {
                returnedSoftwares = softwares.ToList(),
                userClaims = ((ClaimsIdentity)User.Identity).Claims,
                softwareTypes = softwareTypeList,
                recentlyViewed = _dbContext.user_ViewHistories
                    .Where(u_v => u_v.EmployeeId == currentlySignedInUser.EmployeeID)
                    .OrderByDescending(u_v => u_v.time)
                    .Select(u_v => u_v.U_V_Software)
                    .Distinct().Take(6)
                    .ToList()
            };

            // However, if the search string is not empty but the result is then render no result found in the view
            return View(data);
    
        }


        public async Task<IActionResult> GeneratePdf(int softwareId)
        {
            var retrivedSoftware = await _dbContext.Softwares.FindAsync(softwareId);

            System.Diagnostics.Debug.WriteLine($"The Retrieved software name: {retrivedSoftware.SoftwareName}");
            System.Diagnostics.Debug.WriteLine($"The Retrieved software Company ID: {retrivedSoftware.CompanyID}");
            //Retrive the company  information

            var retrivedCompany = await _dbContext.Companies.FindAsync(retrivedSoftware.CompanyID);
            System.Diagnostics.Debug.WriteLine($"The Retrieved Company ID: {retrivedCompany.CompanyID}");
            System.Diagnostics.Debug.WriteLine($"The Retrieved Company name: {retrivedCompany.CompanyName}");

            var retrivedLocationCountries = _dbContext.Company_Country.Where(cc => cc.companyID == retrivedCompany.CompanyID).Select(e => e.country).ToList();

           
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> contactData = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            contactData = CreateContactDataDictionary(retrivedLocationCountries);


            List<string> TypeOfSoftware = _dbContext.Software_Types.Where(st => st.softwareID == retrivedSoftware.SoftwareID).Select(e => e.softwareType.Type).ToList();

            string combinedString = string.Join(",", TypeOfSoftware.ToArray());

            System.Diagnostics.Debug.WriteLine($"The types of software: {string.Join(", ", TypeOfSoftware)}");
            
            // Create a view model to pass data to the service
            var viewModel = new SoftwareCompanyVM
            {
                SoftwareName = retrivedSoftware.SoftwareName,
                CompanyName = retrivedCompany.CompanyName,
                CompanyWebsite = retrivedCompany.WebsiteURL,   
                SoftwareDescription = retrivedSoftware.Description,
                CompanyEstablished = retrivedCompany.EstablishedDate,
                NumberOfEmployees = retrivedCompany.NumberOfEmployee,
                InternalProfessionalServices = retrivedCompany.InternalProfessionalServices,
   
                Cloud = retrivedSoftware.Cloud,
                rating = retrivedSoftware.rating,
                WebsiteURL = retrivedCompany.WebsiteURL,
                CompanyDescription = retrivedCompany.Description,
                

                //List
                //Inorder for the data to be displayed in a single cell, It can be converted into an array of values

                TypeOfSoftware = _dbContext.Software_Types.Where(st => st.softwareID == retrivedSoftware.SoftwareID).Select(e => e.softwareType.Type).ToList(),
                
                BusinessAreas = _dbContext.Software_Areas.Where(s_a => s_a.softwareID == retrivedSoftware.SoftwareID).Select(s_a => s_a.businessArea.Description).ToList(),   
                Modules = _dbContext.Software_Modules.Where(s_m => s_m.softwareID == retrivedSoftware.SoftwareID).Select(s_m => s_m.softwareModule.Module).ToList(),
                FinancialServicesClientTypes = _dbContext.Software_FinancialServicesClientTypes.Where(s_f => s_f.softwareID == retrivedSoftware.SoftwareID).Select(s_f => s_f.financialServicesClientType.Description).ToList(),
                
                //Data will be processed in the ExcelGenerationService
                CompanyContactData = contactData,

            };
            


            
            var pdfBytes = _pdfGenerationService.GeneratePdf(viewModel);
            
            // You can return the PDF as a FileResult
            return File(pdfBytes, "application/pdf", "CompanyReport.pdf");
        }



        public IActionResult Details(int softwareid) {

            System.Diagnostics.Debug.WriteLine($"requested software id: {softwareid}");


            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentlySignedInUser = _dbContext.Employees.FirstOrDefault(e => e.Email == userEmail);

           

            var software = _dbContext.Softwares.FirstOrDefault(s =>s.SoftwareID == softwareid); //get the first software in database

            System.Diagnostics.Debug.WriteLine($"{software?.SoftwareName}");

            int companyId = software.CompanyID;
            var businessAreasList = _dbContext.Software_Areas.Where(s_a => s_a.softwareID == softwareid).Select(s_a => s_a.businessArea).ToList();
            //var typeList = _dbContext.Software_Types.Where(s_t => s_t.softwareID == softwareId).Select(s_t => s_t.softwareType).ToList();
            //var financialServicesList = _dbContext.Software_FinancialServicesClientTypes.Where(s_fs => s_fs.softwareID == softwareId).Select(s_fs => s_fs.financialServicesClientType).ToList();    

            //The company can be a multinational company (have branch in many country)
            var countries = _dbContext.Company_Country.Where(c_c => c_c.companyID  == companyId).Select(c_c => c_c.country).ToList();

            //In a country, the company can have a lot of branches throughout the cities (within the country)
            //In each branch (in the city), they can have a contact number
            
            //MIGHT NEED TO CHANGE THE NAME FOR THE DICTIONARY
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> contactData = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            contactData = CreateContactDataDictionary(countries);

            foreach(var business in businessAreasList)
            {
                System.Diagnostics.Debug.WriteLine($"{business.Description}");
            }


            // Print all values Testing purposes
            foreach (var country in contactData)
            {
                System.Diagnostics.Debug.WriteLine($"Country: {country.Key}");
                //Console.WriteLine($"Person: {country.Key}");

                foreach (var city in country.Value)
                {
                    System.Diagnostics.Debug.WriteLine($"   City: {city.Key}");
                    //Console.WriteLine($"  Category: {city.Key}");

                    foreach (var detail in city.Value)
                    {
                        System.Diagnostics.Debug.WriteLine($"       {detail.Key}: {detail.Value}");
                        //Console.WriteLine($"    {detail.Key}: {detail.Value}");
                    }
                }
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine(" ");
            }

            var similarSoftwares = SimilarSoftware(software.SoftwareID);
            var data = _dbContext.Softwares.Where(s => s.SoftwareID == softwareid).Include(e => e.Company)
           .Include(e => e.reviews)
           .Select(e =>
           new SoftwareVM
           {
               softwareID = e.SoftwareID,
               SoftwareName = e.SoftwareName,
               softwareRating = e.rating,
               SoftwareDescription = e.Description,
               businessAreas = businessAreasList,
               websiteURL = e.Company.WebsiteURL,
               CompanyId = e.Company.CompanyID,
               CompanyName = e.Company.CompanyName,
               companyEstablishedDate = e.Company.EstablishedDate,
               DocumentAttached = e.DocumentAttached,
               companyContactData = contactData,
               reviews = e.reviews,
               newReview = new Review(),
               ImagePath = e.ImagePath,
               similarSoftwares = similarSoftwares
           }
           ).FirstOrDefault();


            //Log user's view in user_viewHistory
            LogUserHistory(software, currentlySignedInUser);



            return View(data);
        }

        private void LogUserHistory(Software software, Employee loggedInUser)
        {
            _dbContext.user_ViewHistories.Add(new User_ViewHistory
            {
                U_V_Employee = loggedInUser,
                EmployeeId = loggedInUser.EmployeeID,
                SoftwareId = software.SoftwareID,   
                time = DateTime.Now,
            });
            _dbContext.SaveChanges();
        }

        [HttpPost]
        public IActionResult AddComment(string userReview, int ratingStar, int softwareId)
        {
            
            System.Diagnostics.Debug.WriteLine("Drop Comment function is running");
            System.Diagnostics.Debug.WriteLine("The received string is: " + userReview);

            System.Diagnostics.Debug.WriteLine("The received rating star is: " + ratingStar);
            System.Diagnostics.Debug.WriteLine("The received software id is: " + softwareId);

            //Assuming the second user drop the comment
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _dbContext.Employees.FirstOrDefault(e => e.Email == userEmail);

            var softwareInview = _dbContext.Softwares.FirstOrDefault(e => e.SoftwareID == softwareId);

            System.Diagnostics.Debug.WriteLine("Current Software in view from passed model: " + softwareId);
            System.Diagnostics.Debug.WriteLine("Current Software in view queried from db: " + softwareInview.SoftwareID);

            //Create the review and send it to database
            Review review = new Review()
            {
                employee = currentUser,
                EmployeeID = currentUser.EmployeeID,
                employeeFirstName = currentUser.FirstName,
                employeeLastName = currentUser.LastName,
                software = softwareInview,
                SoftwareID = softwareInview.SoftwareID,
                givenStar = ratingStar,
                Description = userReview,
                ReviewDate = DateTime.Now,
            };

            
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();


            updateSoftwareRating(softwareInview);

            System.Diagnostics.Debug.WriteLine("The received software id is: " + softwareId);
            // If the model state is not valid, redisplay the form with errors
            return RedirectToAction("Details", new { softwareId = softwareId });
        
        }

        private void updateSoftwareRating(Software software)
        {
            //Extract all review that has softwareId==1 from the reviews table

            var softwareRating = _dbContext.Reviews.Where(r => r.SoftwareID == software.SoftwareID).ToList();
            //Calculate new rating sum of givenStar / number of returned entries
            float newrating = 0;

            foreach (var givenrating in softwareRating)
            {
                newrating += givenrating.givenStar;

                System.Diagnostics.Debug.WriteLine($"The given rating: {givenrating.givenStar}");
                System.Diagnostics.Debug.WriteLine($"The rating: {newrating}");
            }

            newrating = (newrating / softwareRating.Count);

            System.Diagnostics.Debug.WriteLine($"The new rating: {newrating}");
            System.Diagnostics.Debug.WriteLine($"The Number of rating: {softwareRating.Count}");

            //update rating in database
            software.rating = newrating;
            _dbContext.Update(software);
            _dbContext.SaveChanges();

        }

        public async Task<IActionResult> DownloadExcel(int softwareId)
        {
            //For testing purposes, the first excel document for the first software is tested
            System.Diagnostics.Debug.WriteLine($"Generating excel document function is running");
            System.Diagnostics.Debug.WriteLine($"The given software id: {softwareId}");

            
            // Retrieve software information from the database
            var retrivedSoftware = await _dbContext.Softwares.FindAsync(softwareId);

            System.Diagnostics.Debug.WriteLine($"The Retrieved software name: {retrivedSoftware.SoftwareName}");
            System.Diagnostics.Debug.WriteLine($"The Retrieved software Company ID: {retrivedSoftware.CompanyID}");
            //Retrive the company  information

            var retrivedCompany = await _dbContext.Companies.FindAsync(retrivedSoftware.CompanyID);
            System.Diagnostics.Debug.WriteLine($"The Retrieved Company ID: {retrivedCompany.CompanyID}");
            System.Diagnostics.Debug.WriteLine($"The Retrieved Company name: {retrivedCompany.CompanyName}");

            var retrivedLocationCountries = _dbContext.Company_Country.Where(cc => cc.companyID == retrivedCompany.CompanyID).Select(e => e.country).ToList();

           
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> contactData = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            contactData = CreateContactDataDictionary(retrivedLocationCountries);


            List<string> TypeOfSoftware = _dbContext.Software_Types.Where(st => st.softwareID == retrivedSoftware.SoftwareID).Select(e => e.softwareType.Type).ToList();

            string combinedString = string.Join(",", TypeOfSoftware.ToArray());

            System.Diagnostics.Debug.WriteLine($"The types of software: {string.Join(", ", TypeOfSoftware)}");
            
            // Create a view model to pass data to the service
            var viewModel = new SoftwareCompanyVM
            {
                SoftwareName = retrivedSoftware.SoftwareName,
                CompanyName = retrivedCompany.CompanyName,
                CompanyWebsite = retrivedCompany.WebsiteURL,   
                SoftwareDescription = retrivedSoftware.Description,
                CompanyEstablished = retrivedCompany.EstablishedDate,
                NumberOfEmployees = retrivedCompany.NumberOfEmployee,
                InternalProfessionalServices = retrivedCompany.InternalProfessionalServices,
                Cloud = retrivedSoftware.Cloud,

                //List
                //Inorder for the data to be displayed in a single cell, It can be converted into an array of values

                TypeOfSoftware = _dbContext.Software_Types.Where(st => st.softwareID == retrivedSoftware.SoftwareID).Select(e => e.softwareType.Type).ToList(),
                
                BusinessAreas = _dbContext.Software_Areas.Where(s_a => s_a.softwareID == retrivedSoftware.SoftwareID).Select(s_a => s_a.businessArea.Description).ToList(),   
                Modules = _dbContext.Software_Modules.Where(s_m => s_m.softwareID == retrivedSoftware.SoftwareID).Select(s_m => s_m.softwareModule.Module).ToList(),
                FinancialServicesClientTypes = _dbContext.Software_FinancialServicesClientTypes.Where(s_f => s_f.softwareID == retrivedSoftware.SoftwareID).Select(s_f => s_f.financialServicesClientType.Description).ToList(),
                
                //Data will be processed in the ExcelGenerationService
                CompanyContactData = contactData,

            };
            


            // Generate the Excel file
            var excelBytes = await _excelGenerationService.GenerateExcelAsync(viewModel);

            // Return the Excel file as a download
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "software_report.xlsx");



            //For testing purpose redirect to the same page
            //return RedirectToAction("Details", new { id = softwareId });
        }

        //To be checked
        /// <summary>
        /// //////////////////////
        /// </summary>
        /// <returns></returns>
        public List<Software> SimilarSoftware(int currentSoftwareId)
        {
            //Get the current software type tag
            List<string> similarTypeTags = _dbContext.Software_Types.Where(s_t => s_t.softwareID == currentSoftwareId).Select(s_t => s_t.softwareType.Type).ToList();
            
            List<Software> similarSoftware = new List<Software>();
            //Return similar software
            similarSoftware = _dbContext.Software_Types
             .Where(s => similarTypeTags.Contains(s.softwareType.Type)).Select(s_t => s_t.software) //Find all software that has tag as in similarTypeTag
             .Where(s => s.SoftwareID != currentSoftwareId).Distinct()
             .Take(3) //Take the top 3
             .ToList();

            if(similarSoftware.Count < 1)
            {
                //If no similar software (not even one)
                //Search based on business areas
                List<string> businessAreasOfCurrentSoftware = _dbContext.Software_Areas.Where(s_t => s_t.softwareID == currentSoftwareId).Select(s_t => s_t.businessArea.Description).ToList();


                similarSoftware = _dbContext.Software_Areas.Where(s_a => businessAreasOfCurrentSoftware.Contains(s_a.businessArea.Description)).Select(s_a => s_a.software).Where(s => s.SoftwareID != currentSoftwareId).Distinct().Take(3).ToList();   

            }

            return similarSoftware;

        }


        //Create contact data dictionary functions
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> CreateContactDataDictionary(List<Country> countries)
        {
            //This function is to create a contact dictionary

            /*
             Visualizing the structure of the contact data dictionary  

            ContactData = [
                        "Country 1": [

                             "City 1": [
                                "Contact Number": number,
                                "Address": address
                                ],

                             "City 2": [
                                "Contact Number": number,
                                "Address": address
                                ],
                                . 
                                .
                                .

                            "City n": [
                                        "Contact Number": number,
                                        "Address": address
                                ],

                         ],

                        "Country 2": [

                             "City 1": [
                                "Contact Number": number,
                                "Address": address
                                ],

                             "City 2": [
                                "Contact Number": number,
                                "Address": address
                                ],
                                . 
                                .
                                .

                            "City n": [
                                        "Contact Number": number,
                                        "Address": address
                                ],

                         ],
            ]
             
             
             */

            //Contact data dictionary implementation
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> contactData = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            //For each country in this list generate a city dictionary
            foreach (Country country in countries)
            {
                //City dictionary
                // Dictionary<city, Dictionary<contact, address>> cityDictionary
                Dictionary<string, Dictionary<string, string>> cityDictionary = new Dictionary<string, Dictionary<string, string>>();

                cityDictionary = CreateCountryDictionary(country);

                contactData.Add(country.CountryName, cityDictionary); //add to the contact data dictionary 
            }


            return contactData;
        }

        private Dictionary<string, Dictionary<string, string>> CreateCountryDictionary(Country country)
        {
            //Based on the given country, create a city dictionary
            //Dictionary<cityName, Dictionary<contact, address>>
            Dictionary<string, Dictionary<string, string>> cityDictionary = new Dictionary<string, Dictionary<string, string>>();


            //Step 1: Get all the cities in the country
            var citiesInCountry = _dbContext.Cities.Where(city => city.CountryID == country.CountryID).ToList();

            //Step 2: For each of the city, create a dictionary to store the contact number and address

            foreach (City city in citiesInCountry)
            {
                Dictionary<string, string> contact = new Dictionary<string, string>();

                //Contact dictionary consist of contact number and address 
                //NOT SIMILAR TO CONTACT DATA
                contact = GenerateContactDictionary(city);
                cityDictionary.Add(city.CityName, contact);
            }
            return cityDictionary;
        }

        private Dictionary<string, string> GenerateContactDictionary(City c)
        {
            Dictionary<string, string> contact = new Dictionary<string, string>();


            //Get contact number
            var contactNumber = _dbContext.ContactNumbers.FirstOrDefault(a => a.CityID.Equals(c.CityID));

            if (contactNumber != null)
            {
                contact.Add("Contact Number", (contactNumber.Number).ToString());
            }


            //Get address
            var address = _dbContext.Addresses.FirstOrDefault(a => a.CityID == c.CityID);

            if (address != null)
            {
                //Concatenate the address attributes 
                string concatenatedAddress = address.AddressLine1 + " " + address.AddressLine2 + " " + address.State + " " + address.PostCode + " " + address.city.CityName;
                contact.Add("Address", concatenatedAddress);
            }

            return contact;
        }

    }

}

/*
 
 
 public List<Country> countryList { get; set; }
        public List<City> cityList { get; set; }
        public List<ContactNumber> contactNumberList { get; set; }  
        public List<BusinessArea> businessAreas { get; set; }
        public List<Review> reviews { get; set; }
        public List<Software> similarSoftwares { get; set; }
 */