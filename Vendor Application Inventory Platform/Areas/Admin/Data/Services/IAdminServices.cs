using Vendor_Application_Inventory_Platform.Models;
namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

public interface IAdminServices
{
    
    List<string> CountryNamesByCompany(int companyId);

    Company? FindCompanyById(int companyId);

    public void CreateNewCountry(string countryName, Company company);

    public Country RetrieveCountry(string countryName);

    public void DeleteCountry(string countryName, int companyId);

    public City CreateNewCity(string cityName, Country country);

    List<City> ListCities(int companyId, string countryName);

    City? FindCity(int companyId, string countryName, string cityName);

    public void DeleteCity(City city);

    public void CreateNewContact(string contactNum, City? city);

    public void CreateNewAddress(string? address1, string? address2, string? postcode, string? state, City city);

    public void DeleteContact(City city);

    public void DeleteAddress(City city);

    public Address? GetAddress(int cityId);
    public ContactNumber? GetContact(int cityId);
    
}