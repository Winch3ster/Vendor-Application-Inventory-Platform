using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
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
        public SoftwareController(AppDbContext dbContext, IExcelGenerationService excelGenerationService)
        {
            _dbContext = dbContext;
            _excelGenerationService = excelGenerationService;
        }


        public IActionResult Index(string searchString)
        {
            
            System.Diagnostics.Debug.WriteLine(searchString);
              
            var softwares = from i in _dbContext.Softwares
                            select i;
            //If the search string is empty, then return all from database
            //If the search string IS NOT EMPTY, return the result
            if (!string.IsNullOrEmpty(searchString))
            {

                softwares = softwares.Where(s => s.SoftwareName.Contains(searchString) || s.Description.Contains(searchString));
            }

            //However, if the search string is not empty but the result is then render no result found in the view
            return View(softwares.ToList());
    
        }





        public IActionResult Details(int softwareId) { 
            var software = _dbContext.Softwares.FirstOrDefault(s =>s.SoftwareID == 1); //get the first software in database

            System.Diagnostics.Debug.WriteLine($"{software.SoftwareName}");

            int companyId = software.CompanyID;
            var businessAreasList = _dbContext.Software_Areas.Where(s_a => s_a.softwareID == 1).Select(s_a => s_a.businessArea).ToList();
            //var typeList = _dbContext.Software_Types.Where(s_t => s_t.softwareID == softwareId).Select(s_t => s_t.softwareType).ToList();
            //var financialServicesList = _dbContext.Software_FinancialServicesClientTypes.Where(s_fs => s_fs.softwareID == softwareId).Select(s_fs => s_fs.financialServicesClientType).ToList();    

            //The company can be a multinational company (have branch in many country)
            var countries = _dbContext.Company_Country.Where(c_c => c_c.companyID  == companyId).Select(c_c => c_c.country).ToList();

            //In a country, the company can have alot of branches throyghout the cities (within the country)
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


            var data = _dbContext.Softwares.Where(s => s.SoftwareID == 1).Include(e => e.Company)
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
               CompanyName = e.Company.CompanyName,
               companyEstablishedDate = e.Company.EstablishedDate,
               companyContactData = contactData,
               reviews = e.reviews,
               newReview = new Review()
           }
           ).FirstOrDefault();

            Console.WriteLine(data);

            return View(data);
        }


        [HttpPost]
        public IActionResult AddComment(string userReview, int ratingStar, int softwareId)
        {
            
            //System.Diagnostics.Debug.WriteLine("Drop Comment function is running");
            //System.Diagnostics.Debug.WriteLine("The received string is: " + userReview);

            System.Diagnostics.Debug.WriteLine("The received rating star is: " + ratingStar);
            
            //Assuming the second user drop the comment
            // Retrieve the current user's ID
            var currentUser = _dbContext.Employees.FirstOrDefault(e => e.EmployeeID == 1);
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
            // If the model state is not valid, redisplay the form with errors
            return RedirectToAction("Details", new { id = softwareId });
        
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
                LastDemoDate = retrivedCompany.LastDemoDate,
                LastReviewDate = retrivedCompany.LastReviewDate,
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
             .Take(3) //Take the top 3
             .ToList();

            if(similarSoftware.Count < 1)
            {
                //If no similar software (not even one)
                //Search based on business areas
                List<string> businessAreasOfCurrentSoftware = _dbContext.Software_Areas.Where(s_t => s_t.softwareID == currentSoftwareId).Select(s_t => s_t.businessArea.Description).ToList();


                similarSoftware = _dbContext.Software_Areas.Where(s_a => businessAreasOfCurrentSoftware.Contains(s_a.businessArea.Description)).Select(s_a => s_a.software).Take(3).ToList();   

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