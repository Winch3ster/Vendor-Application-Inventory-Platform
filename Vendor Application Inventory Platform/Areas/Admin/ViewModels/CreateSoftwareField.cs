namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class CreateSoftwareField
{
    public string CompanyID { get; set; }
    public string SoftwareName { get; set; }
    public string Description { get; set; }
    public string CloudOption { get; set; }
    public List<int> Modules { get; set; }
    public List<int> SoftwareTypes { get; set; }
    public List<int> BusinessArea { get; set; }
    public List<int> FinancialServiceClientType { get; set; }
}