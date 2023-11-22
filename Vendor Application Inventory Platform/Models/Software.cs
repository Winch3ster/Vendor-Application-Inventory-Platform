using System.ComponentModel.DataAnnotations;
using Vendor_Application_Inventory_Platform.Data.Enum;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software
    {
        public int SoftwareID { get; set; }

        //Foreign key
        public int CompanyID { get; set; }
        public Company Company { get; set; }

        public string SoftwareName { get; set; }
        public string Description { get; set; }


        public CloudType Cloud { get; set; }

        public bool DocumentAttached { get; set; }

        //Set the software rating by taking the average rating 
        public float rating { get; set; }

        //The software image will be stored in the server. Only the path to the image is stored in the database
        [Required]
        public string ImagePath { get; set; }




        //Entity Relationships
        public List<Software_Area> Software_Areas { get; set; }

        public List<Software_Module> Software_Modules { get; set; }

        public List<Software_Type> Software_Types { get; set; }

        public List<Software_FinancialServicesClientType> Software_FinancialServicesClientTypes { get; set; }


        public List<Review> reviews { get; set; }
        public List<User_ViewHistory> user_ViewHistories { get; set; }

        /*
        //Entity Relationships
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<BusinessArea> BusinessAreas { get; set; }
        public virtual ICollection<SoftwareModule> SoftwareModules { get; set; }

        public virtual ICollection<SoftwareType> SoftwareTypes { get; set; }
        
        
        public Software()
        {
            Reviews = new List<Review>();
            BusinessAreas = new List<BusinessArea>();
            SoftwareModules = new List<SoftwareModule>();
            SoftwareTypes = new List<SoftwareType>();
        }
        */
    }
}
