namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software_Module
    {

        public int Id { get; set; }

        public int moduleID { get; set; }
        public SoftwareModule softwareModule { get; set; }


        public int softwareID { get; set; }
        public Software software { get; set; }
    }
}
