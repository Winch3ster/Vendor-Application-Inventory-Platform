using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

public class AdminServices : IAdminServices
{
    
    private readonly AppDbContext _db;

    public AdminServices(AppDbContext db)
    {
        _db = db;
    }

    public List<string> CountryNamesByCompany(int companyId)
    {
        /*
        var objCountries = _db.Companies
            .Where(c => c.CompanyID == companyId)
            .SelectMany(c => c.Countries)
            .Select(country => country.CountryName).ToList();
        */
        var objCountries = _db.Company_Country
            .Where(cc => cc.companyID == companyId)
            .Select(cc => cc.country.CountryName)
            .ToList();


        return objCountries;
    }

    public Company? FindCompanyById(int id)
    {
        //return _db.Companies.Include(company => company.Company_Countries).FirstOrDefault(c => c.CompanyID == id);
        return _db.Companies.FirstOrDefault(c => c.CompanyID == id);
    }

    public void CreateNewCountry(string countryName, Company company)
    {
        //TO BE EDITED HOW TO SAVE THE DATA THEN????????
        var country = new Country
        {
            CountryName = countryName,
            //Companies = new List<Company> { company }
        };
        _db.SaveChanges();


        _db.Countries.Add(country);
        _db.SaveChanges();
    }

    public Country RetrieveCountry(string countryName)
    {
        return _db.Countries.FirstOrDefault(c => c.CountryName == countryName) ?? throw new InvalidOperationException();
    }

    public void DeleteCountry(string countryName, int companyId)
    {

        //Why delete country need company??

        /*
        var country = _db.Companies
            .Where(c => c.CompanyID == companyId)
            .SelectMany(c => c.Countries)
            .FirstOrDefault(country => country.CountryName == countryName);
        */


        // Step 1: Find the Country entity by name
        var country = _db.Countries.FirstOrDefault(c => c.CountryName == countryName);

        // Step 2: Find the Company entity by ID
        var company = _db.Companies.FirstOrDefault(c => c.CompanyID == companyId);



        // Check if both Country and Company are found
        if (country != null && company != null)
        {
            // Step 3: Identify the Company_Country entry to be removed
            var companyCountry = _db.Company_Country
                .FirstOrDefault(cc => cc.companyID == companyId && cc.countryID == country.CountryID);

            // Step 4: Remove the Company_Country entry
            if (companyCountry != null)
            {
                _db.Company_Country.Remove(companyCountry);

                // Step 5: Optionally, check if the country has any remaining associations
                var remainingAssociations = _db.Company_Country.Any(cc => cc.countryID == country.CountryID);

                // If no remaining associations, delete the Country entry
                if (!remainingAssociations)
                {
                    _db.Countries.Remove(country);
                }

                // Step 6: Save changes to persist the modifications
                _db.SaveChanges();
            }
        }


        //if (country != null) 
        //    _db.Countries.Remove(country);

        //_db.SaveChanges();
        
    }

    public City CreateNewCity(string cityName, Country country)
    {
        var city = new City
        {
            CountryID = country.CountryID,
            CityName = cityName
        };

        country.Cities?.Add(city);
        _db.SaveChanges();

        return city;
    }

    public List<City> ListCities(int companyId, string countryName)
    {
        // 1 company can be in many country
        // and
        // 1 Country can have many companies

        //Now I want to list all the cities that this company is in 

        //Step 1: Select all the country that this company is in
        //Step 2: Based on the selected countries, list the country's cities


        //List cities based on 2 criteria --> company id and country name

        //Step 1: Find all the countries
        return _db.Company_Country.Where(cc => cc.companyID == companyId).Select(cc => cc.country).Where(country => country.CountryName == countryName).SelectMany(country => country.Cities!)
            .ToList();

       
        
        /*
        return _db.Companies
            .Where(c => c.CompanyID == companyId)
            .SelectMany(c => c.Countries)
            .Where(country => country.CountryName == countryName)
            .SelectMany(country => country.Cities!)
            .ToList();
        

       */
    }

    public City FindCity(int companyId, string countryName, string cityName)
    {
        //I want to find a city based on the company, which country it is in and the city name
        //Step 1: Find all countries that this company is in
        //Step 2: Based on the filtered countries, I want to find the country that has 'countryName' name
        //Step 3: Once I have the country I can get the city via its name


        var city = _db.Cities
        .Where(c => c.country.Company_Countries.Any(cc => cc.companyID == companyId) &&
                    c.country.CountryName == countryName &&
                    c.CityName == cityName).FirstOrDefault();

        return city;


        /*
        return _db.Companies
            .Where(company=>company.CompanyID==companyId)
            .SelectMany(company=>company.Countries)
            .Where(country=>country.CountryName==countryName)
            .SelectMany(country=>country.Cities)
            .FirstOrDefault(city=>city.CityName==cityName);
        */
    }

    public void DeleteCity(City city)
    {
        _db.Cities.Remove(city);
        _db.SaveChanges();
    }

    public void CreateNewContact(string contactNum, City city)
    {
        ContactNumber? contactExist = _db.ContactNumbers.FirstOrDefault(c => c.CityID == city.CityID);

        if (contactExist != null)
        {
            contactExist.Number = int.Parse(contactNum);
            _db.ContactNumbers.Update(contactExist);
        }
        else
        {
            var contact = new ContactNumber()
            {
                CityID = city.CityID,
                Number = int.Parse(contactNum)
            };

            _db.ContactNumbers.Add(contact);
        }
        _db.SaveChanges();
    }
    
    public void CreateNewAddress(string? address1, string? address2, string? postcode, City city)
    {
        Address? addressExist = _db.Addresses.FirstOrDefault(c => c.CityID == city.CityID);

        if (addressExist != null)
        {
            addressExist.AddressLine1 = address1;
            addressExist.AddressLine2 = address2;
            addressExist.PostCode = postcode;
            _db.Addresses.Update(addressExist);
        }
        else
        {
            var address = new Address()
            {
                CityID = city.CityID,
                AddressLine1 = address1,
                AddressLine2 = address2,
                PostCode = postcode
            };

            _db.Addresses.Add(address);
        }
        _db.SaveChanges();
    }

    public void DeleteContact(City city)
    {
        ContactNumber? contact = _db.ContactNumbers.FirstOrDefault(c => c.CityID == city.CityID);
        if (contact != null)
        {
            _db.ContactNumbers.Remove(contact);
            _db.SaveChanges();
        }
        
    }
    
    public void DeleteAddress(City city)
    {
        Address? address = _db.Addresses.FirstOrDefault(c => c.CityID == city.CityID);
        if (address != null)
        {
            _db.Addresses.Remove(address);
            _db.SaveChanges();
        }
        
    }

    public Address? GetAddress(int cityId)
    {
        return _db.Addresses.FirstOrDefault(address => address.CityID == cityId);
    }

    public ContactNumber? GetContact(int cityId)
    {
        return _db.ContactNumbers.FirstOrDefault(contact => contact.CityID==cityId);
    }
    
}