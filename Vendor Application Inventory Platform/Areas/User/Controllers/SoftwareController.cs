using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;
using Vendor_Application_Inventory_Platform.Views.Employee;

namespace Vendor_Application_Inventory_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class SoftwareController : Controller
    {
        private readonly AppDbContext _dbContext;
        public SoftwareController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }





        public IActionResult Details(int softwareId) { 
            var software = _dbContext.Softwares.FirstOrDefault(s =>s.SoftwareID == 1001); //get the first software in database
            int companyId = software.CompanyID;
            var businessAreasList = _dbContext.Software_Areas.Where(s_a => s_a.softwareID == 1001).Select(s_a => s_a.businessArea).ToList();
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


            var data = _dbContext.Softwares.Where(s => s.SoftwareID == 1001).Include(e => e.Company)
           .Include(e => e.reviews)
           .Select(e =>
           new SoftwareVM
           {
               softwareID = e.SoftwareID,
               SoftwareName = e.SoftwareName,
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

        //Create contact data dictionary 
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
            foreach (Country country in countries) {
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
            
            if(contactNumber != null)
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


        [HttpPost]
        public IActionResult AddComment(string userReview, int softwareId)
        {
            
            System.Diagnostics.Debug.WriteLine("Drop Comment function is running");
            System.Diagnostics.Debug.WriteLine("The received string is: " + userReview);
            //Assuming the second user drop the comment
            // Retrieve the current user's ID
            var currentUser = _dbContext.Employees.FirstOrDefault(e => e.EmployeeID == 1003);
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
                Description = userReview,
                ReviewDate = DateTime.Now,
            };

            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();



            // If the model state is not valid, redisplay the form with errors
            return RedirectToAction("Details", new { id = softwareId });
        
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