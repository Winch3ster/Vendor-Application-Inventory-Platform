namespace Vendor_Application_Inventory_Platform.Models
{

    //This is about to be deleted
    public class SoftwareBusinessArea
    {
        public int SoftwareID { get; set; }
        public Software Software { get; set; }

        public int BusinessAreaID { get; set; }
        public BusinessArea BusinessArea { get; set; }
    }
}
