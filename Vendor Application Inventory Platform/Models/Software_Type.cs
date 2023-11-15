namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software_Type
    {
        public int Id { get; set; }

        public int typeID { get; set; }
        public SoftwareType softwareType { get; set; }


        public int softwareID { get; set; }
        public Software software { get; set; }

    }
}
