using System.Reflection.Metadata.Ecma335;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        public List<Company_Country> Company_Countries { get; set; }

        public ICollection<City>? Cities { get; set; }

        /*
        public virtual ICollection<Company> Companies { get; set; }
        
        
        public Country()
        {
            // Initialize the navigation properties in the constructor
            Companies = new List<Company>();
            Cities = new List<City>();
        }
        */

    }
}
