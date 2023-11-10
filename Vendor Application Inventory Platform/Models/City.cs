namespace Vendor_Application_Inventory_Platform.Models
{
    public class City
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public string CityName { get; set; }


        public virtual Country Country { get; set; }
        public virtual ContactNumber ContactNumber { get; set; }
        public virtual Address Address { get; set; }

    }
}
