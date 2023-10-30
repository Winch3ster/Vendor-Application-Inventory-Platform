using System.ComponentModel.DataAnnotations;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Review
    {

        public int ReviewID { get; set; }

        public int EmployeeID { get; set; }

        public int SoftwareID { get; set; }
        public int Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReviewDate { get; set; }


        public virtual Employee Employee { get; set; }

        public virtual Software Software { get; set; }
    }
}
