namespace Vendor_Application_Inventory_Platform.Models
{
    public class SoftwareModule
    {
        public int SoftwareModuleID { get; set; }
        public string Module { get; set; }


        public virtual ICollection<Software> Softwares { get; set; }
    }
}
