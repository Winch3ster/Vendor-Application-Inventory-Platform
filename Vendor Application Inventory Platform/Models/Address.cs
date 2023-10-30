namespace Vendor_Application_Inventory_Platform.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }
        public int PostCode { get; set;}
    }
}
