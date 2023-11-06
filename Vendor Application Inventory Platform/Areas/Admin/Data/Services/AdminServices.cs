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
        return _db.Companies.Include(company => company.Countries).FirstOrDefault(c => c.CompanyID ==id);
    }

    public List<string> CountryNamesInDb()
    {
        return _db.Countries.Select(country => country.CountryName).ToList();
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

    public Country CountryExistOrNot(string countryName)
    {
        return _db.Countries.FirstOrDefault(c => c.CountryName == countryName) ?? throw new InvalidOperationException();
    }

    public void ConnectCountryToCompany(Company company, Country country)
    {
        company.Countries.Add(country);
        _db.SaveChanges();
    }
    
    public void DeleteCountry(string countryName, int companyId)
    {
        var country = _db.Countries.FirstOrDefault(c => c.CountryName == countryName);
        if (country != null)
        {
            var company = _db.Companies.Include(company => company.Countries)
                .FirstOrDefault(c => c.CompanyID == companyId);
            if (company != null)
            {
                company.Countries.Remove(country);
            }
            _db.SaveChanges();
        }
    }
}