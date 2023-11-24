using Vendor_Application_Inventory_Platform.Data.Enum;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class ChangeLog
    {
        //This model is used to record a history of changes made to software and company table.
        //This will track changes like CREATE, UPDATE, DELETE


        public int ChangeLogId { get; set; }
        public string EntityName { get; set; }
        public Actions ActionPerformed { get; set; }
        public DateTime time { get; set; }
    }
}
