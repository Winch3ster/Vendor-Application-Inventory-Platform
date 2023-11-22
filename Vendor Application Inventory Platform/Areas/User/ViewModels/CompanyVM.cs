using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.User.ViewModels
{
    public class CompanyVM
    {
        public Company CVM_Company { get; set; }

        public List<Software> CVM_Software { get; set; }

        //The contactData consist of location countries, cities, contact number and address
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> contactData { get; set; }
    }
}
