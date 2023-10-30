namespace Vendor_Application_Inventory_Platform.Models
{
    public class BusinessArea
    {
        public int BusinessAreaID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
    }
}
