using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

public class CompanyServices : ICompanyServices
{
    
    private readonly AppDbContext _db;

    public CompanyServices(AppDbContext db)
    {
        _db = db;
    }

    public List<string> CountryNamesByCompany(int companyId)
    {
        
        var objCountries = _db.Company_Country
            .Where(cc => cc.companyID == companyId)
            .Select(cc => cc.country.CountryName)
            .ToList();


        return objCountries;
    }

    public Company? FindCompanyById(int id)
    {
        return _db.Companies.Include(company => company.Company_Countries).FirstOrDefault(c => c.CompanyID == id);
    }

    public void CreateNewCountry(string countryName, Company company)
    {
        var country = new Country
        {
            CountryName = countryName,
            Company_Countries = new List<Company_Country> { new Company_Country { companyID = company.CompanyID } }
        };


        _db.Countries.Add(country);
        _db.SaveChanges();
    }

    public Country RetrieveCountry(string countryName)
    {
        return _db.Countries.FirstOrDefault(c => c.CountryName == countryName) ?? throw new InvalidOperationException();
    }

    public void DeleteCountry(string countryName, int companyId)
    {
        
        var companyCountry = _db.Company_Country
            .FirstOrDefault(cc => cc.companyID == companyId && cc.country.CountryName == countryName);

        if (companyCountry != null)
        {
            _db.Company_Country.Remove(companyCountry);

            var country = _db.Countries.FirstOrDefault(c => c.CountryID == companyCountry.countryID);
            if (country != null)
            {
                _db.Countries.Remove(country);
            }

            _db.SaveChanges();
        }
        
    }

    public City CreateNewCity(string cityName, Country country)
    {
        var city = new City
        {
            CountryID = country.CountryID,
            CityName = cityName
        };

        _db.Cities.Add(city);
        _db.SaveChanges();

        return city;
    }

    public List<City> ListCities(int companyId, string countryName)
    {
        
        var cities = _db.Company_Country
            .Where(cc => cc.companyID == companyId && cc.country.CountryName == countryName)
            .SelectMany(cc => cc.country.Cities!)
            .ToList();

        return cities;
    }

    public City? FindCity(int companyId, string countryName, string cityName)
    {
        var city = _db.Company_Country
            .Where(cc => cc.companyID == companyId && cc.country.CountryName == countryName)
            .SelectMany(cc => cc.country.Cities!)
            .FirstOrDefault(city => city.CityName == cityName);

        return city;
    }

    public void DeleteCity(City city)
    {
        _db.Cities.Remove(city);
        _db.SaveChanges();
    }

    public void CreateNewContact(string contactNum, City? city)
    {
        ContactNumber? contactExist = _db.ContactNumbers.FirstOrDefault(c => c.CityID == city.CityID);

        if (contactExist != null)
        {
            contactExist.Number = long.Parse(contactNum);
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
    
    public void CreateNewAddress(string? address1, string? address2, string? postcode, string? state, City city)
    {
        Address? addressExist = _db.Addresses.FirstOrDefault(c => c.CityID == city.CityID);

        if (addressExist != null)
        {
            addressExist.AddressLine1 = address1;
            addressExist.AddressLine2 = address2;
            addressExist.PostCode = postcode;
            addressExist.State = state;
            _db.Addresses.Update(addressExist);
        }
        else
        {
            var address = new Address()
            {
                CityID = city.CityID,
                AddressLine1 = address1,
                AddressLine2 = address2,
                PostCode = postcode,
                State = state
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

    public Company CreateNewCompany(CreateCompanyField createCompanyField)
    {
        var newCompany = new Company
        {
            CompanyName = createCompanyField.CompanyName,
            WebsiteURL = createCompanyField.WebsiteURL,
            Description = createCompanyField.Description,
            EstablishedDate = DateTime.Parse(createCompanyField.EstablishedDate),
            NumberOfEmployee = int.Parse(createCompanyField.NumberOfEmployee),
            InternalProfessionalServices = bool.Parse(createCompanyField.InternalProfessionalServices),
            LastDemoDate = DateTime.Parse(createCompanyField.LastDemoDate),
            LastReviewDate = DateTime.Parse(createCompanyField.LastReviewDate) 
        };

        _db.Companies.Add(newCompany);
        _db.SaveChanges();
        return newCompany;
    }

    public Company? UpdateCompany(int companyId, CreateCompanyField createCompanyField)
    {
        var company = _db.Companies.FirstOrDefault(c => c.CompanyID == companyId);
        
        if (company != null)
        {
            company.CompanyName = createCompanyField.CompanyName;
            company.WebsiteURL = createCompanyField.WebsiteURL;
            company.Description = createCompanyField.Description;
            company.EstablishedDate = DateTime.Parse(createCompanyField.EstablishedDate);
            company.NumberOfEmployee = int.Parse(createCompanyField.NumberOfEmployee);
            company.InternalProfessionalServices = bool.Parse(createCompanyField.InternalProfessionalServices);
            company.LastDemoDate = DateTime.Parse(createCompanyField.LastDemoDate);
            company.LastReviewDate = DateTime.Parse(createCompanyField.LastReviewDate);

            _db.SaveChanges();
        }
        
        return company;
    }
    
}