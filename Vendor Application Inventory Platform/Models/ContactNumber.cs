using System.Reflection.Metadata.Ecma335;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class ContactNumber
    {
        public int ContactNumberID { get; set; }
        public long Number { get; set; }
        public int CityID { get; set; }

        public virtual City city { get; set; }

    }
}
