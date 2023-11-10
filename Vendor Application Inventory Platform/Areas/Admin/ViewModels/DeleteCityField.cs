namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class DeleteCityField
{
    public List<DeleteCity>? deleteCities { get; set; }
}

public class DeleteCity
{
    public string Country { get; set; }
    public string City { get; set; }
}