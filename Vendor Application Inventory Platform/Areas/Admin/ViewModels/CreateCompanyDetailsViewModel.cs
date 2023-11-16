namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class CreateCompanyDetailsViewModel
{
    public int companyId { get; set; }
    public List<CountryViewModel> Countries { get; set; }

    public class CountryViewModel
    {
        public string CountryName { get; set; }
        public List<CityViewModel> Cities { get; set; }
    }
    
    public class CityViewModel
    {
        public string CityName { get; set; }
        public AddressViewModel Address { get; set; }
        public ContactViewModel Contact { get; set; }

    }

    public class AddressViewModel
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PostCode { get; set; }
        public string? State { get; set; }
    }

    public class ContactViewModel
    {
        public long? ContactNumber { get; set; }
        
    }
}