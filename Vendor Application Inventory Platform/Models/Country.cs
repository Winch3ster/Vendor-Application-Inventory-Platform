using System.Reflection.Metadata.Ecma335;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<City>? Cities { get; set; }
        
        public Country()
        {
            // Initialize the navigation properties in the constructor
            Companies = new List<Company>();
            Cities = new List<City>();
        }

    }
}
