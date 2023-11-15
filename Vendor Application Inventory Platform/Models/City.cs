namespace Vendor_Application_Inventory_Platform.Models
{
    public class City
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public string CityName { get; set; }

      
        public Country country { get; set; }
        public ContactNumber contactNumber { get; set; }
        public Address address { get; set; }

    }
}
