using Vendor_Application_Inventory_Platform.Data.Enum;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string serviceClientType { get; set; }

        public CloudType cloud { get; set; }

    }
}
