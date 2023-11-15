namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software_Area
    {
        public int Id { get; set; }

        public int areaID { get; set; }
        public BusinessArea businessArea { get; set; }


        public int softwareID { get; set; }
        public Software software { get; set; }
    }
}
