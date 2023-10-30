namespace Vendor_Application_Inventory_Platform.Models
{
    public class SoftwareType
    {
        public int SoftwareTypeID { get; set; }
        public int SoftwareID { get; set; }
        public string Type { get; set; }


        public virtual ICollection<Software> Softwares { get; set; }
    }
}
