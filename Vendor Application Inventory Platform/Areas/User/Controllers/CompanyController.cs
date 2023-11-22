using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Vendor_Application_Inventory_Platform.Areas.User.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class CompanyController : Controller
    {
        public readonly AppDbContext _dbContext;
        public CompanyController(AppDbContext dbcontext)
        {
                _dbContext = dbcontext;
        }


        public IActionResult Details(int companyId)
        {
            System.Diagnostics.Debug.WriteLine(companyId.ToString());
            //Return the requested company
            var returnedCompany = _dbContext.Companies.FirstOrDefault(c => c.CompanyID == companyId);

            //Get all the software that is developed by this company
            var softwareByCompany = _dbContext.Softwares.Where(s => s.CompanyID == companyId).ToList();

            var retrivedLocationCountries = _dbContext.Company_Country.Where(cc => cc.companyID == returnedCompany.CompanyID).Select(e => e.country).ToList();


            var contactData = CreateContactDataDictionary(retrivedLocationCountries);

            var data = new CompanyVM()
            {
                CVM_Company = returnedCompany,
                CVM_Software = softwareByCompany,
                contactData = contactData
            };

            return View(data);
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
