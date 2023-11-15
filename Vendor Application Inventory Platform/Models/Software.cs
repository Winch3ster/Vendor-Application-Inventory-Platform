using Vendor_Application_Inventory_Platform.Data.Enum;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software
    {
        public int SoftwareID { get; set; }

        public int CompanyID { get; set; }
        public string SoftwareName { get; set; }
        public string Description { get; set; }


        public CloudType Cloud { get; set; }

        public bool DocumentAttached { get; set; }




        public List<Software_Area> Software_Areas { get; set; }





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
