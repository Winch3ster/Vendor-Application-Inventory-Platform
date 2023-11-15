namespace Vendor_Application_Inventory_Platform.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string WebsiteURL { get; set; }
        public string Description { get; set; }

        public DateTime EstablishedDate { get; set; }


        public int NumberOfEmployee { get; set; }

        public bool InternalProfessionalServices { get; set; }

        public DateTime LastDemoDate { get; set; }
        public DateTime LastReviewDate { get; set; }


        /*
        public virtual ICollection<Software> Softwares { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        
        public Company()
        {
            Softwares = new List<Software>();
            Countries = new List<Country>();
        }
        */

    }
}
