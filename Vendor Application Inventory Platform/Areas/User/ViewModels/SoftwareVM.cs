using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.User.ViewModels
{
    public class SoftwareVM
    {
        //Software Information
        public int softwareID { get; set; }
        public string SoftwareName { get; set; }
        public string SoftwareDescription { get; set; }

        public string websiteURL { get; set; }

        //Company Information
        public string CompanyName { get; set; }
        public DateTime companyEstablishedDate { get; set; }

        public Review newReview { get; set; }

        //Lists
       
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> companyContactData { get; set; }
        public List<BusinessArea> businessAreas { get; set; }
        public List<Review> reviews { get; set; }
        //public List<Software> similarSoftwares { get; set; }
    }
}
