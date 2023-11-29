using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class SoftwareViewModel
{
    public int SoftwareID { get; set; }
    public string SoftwareName { get; set; }
    public string Description { get; set; }
    public CloudType Cloud { get; set; }
    public bool DocumentAttached { get; set; }


    public List<string> FinancialServicesClientTypes { get; set; }
    public List<string> SoftwareTypes { get; set; }
    public List<string> SoftwareModules { get; set; }
    public List<string> BusinessAreas { get; set; }
}