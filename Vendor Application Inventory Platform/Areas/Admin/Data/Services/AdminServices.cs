using Microsoft.EntityFrameworkCore;
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
        var objCountries = _db.Companies
            .Where(c => c.CompanyID == companyId)
            .SelectMany(c => c.Countries)
            .Select(country => country.CountryName).ToList();

        return objCountries;
    }

    public Company? FindCompanyById(int id)
    {
        return _db.Companies.Include(company => company.Countries).FirstOrDefault(c => c.CompanyID == id);
    }

    public void CreateNewCountry(string countryName, Company company)
    {
        var country = new Country
        {
            CountryName = countryName,
            Companies = new List<Company> { company }
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
        var country = _db.Companies
            .Where(c => c.CompanyID == companyId)
            .SelectMany(c => c.Countries)
            .FirstOrDefault(country => country.CountryName == countryName);

        if (country != null) 
            _db.Countries.Remove(country);

        _db.SaveChanges();
        
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
        return _db.Companies
            .Where(c => c.CompanyID == companyId)
            .SelectMany(c => c.Countries)
            .Where(country => country.CountryName == countryName)
            .SelectMany(country => country.Cities!)
            .ToList();
    }

    public City FindCity(int companyId, string countryName, string cityName)
    {
        return _db.Companies
            .Where(company=>company.CompanyID==companyId)
            .SelectMany(company=>company.Countries)
            .Where(country=>country.CountryName==countryName)
            .SelectMany(country=>country.Cities)
            .FirstOrDefault(city=>city.CityName==cityName);
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