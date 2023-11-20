using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.User.ViewModels
{
    public class SoftwareCompanyVM
    {
        public string SoftwareName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public List<string> TypeOfSoftware { get; set; }
        public string SoftwareDescription { get; set; }
        public DateTime CompanyEstablished { get; set; }

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> CompanyContactData { get; set; }

        public int NumberOfEmployees { get; set; }
        public bool InternalProfessionalServices { get; set; }
        public DateTime LastDemoDate { get; set; }
        public DateTime LastReviewDate { get; set; }
        public List<string> BusinessAreas { get; set; }
        public List<string> Modules { get; set; }
        public List<string> FinancialServicesClientTypes { get; set; }
        public CloudType Cloud { get; set; }

        //The DocumentAttached attribute is not needed as it only indicate whether a specifc software has generate pdf funtionalities or not

    }
}
