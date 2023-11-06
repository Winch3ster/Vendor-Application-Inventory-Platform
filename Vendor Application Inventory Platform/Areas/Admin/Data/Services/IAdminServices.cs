using Vendor_Application_Inventory_Platform.Models;
namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

public interface IAdminServices
{
    List<string> CountryNamesByCompany(int companyId);

    Company? FindCompanyById(int companyId);

    List<string> CountryNamesInDb();

    public void CreateNewCountry(string countryName, Company company);

    public Country CountryExistOrNot(string countryName);

    public void ConnectCountryToCompany(Company company, Country country);

    public void DeleteCountry(string countryName, int companyId);

}