using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class CreateSoftwareViewModel
{
    public List<Company> Companies { get; set; }
    public List<BusinessArea> BusinessAreas { get; set; }
    public List<SoftwareModule> SoftwareModules { get; set; }
    public List<FinancialServicesClientType> FinancialServicesClientTypes { get; set; }
    public List<SoftwareType> SoftwareTypes { get; set; }
    public string ImagePath { get; set; }


}