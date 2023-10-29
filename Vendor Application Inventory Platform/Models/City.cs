namespace Vendor_Application_Inventory_Platform.Models
{
    public class City
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public int ContactID { get; set; }
        public int CityName { get; set; }


        public virtual Country Country { get; set; }
        public virtual ContactNumber ContactNumber { get; set; }
    }
}
