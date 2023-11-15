using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class SoftwareType
    {
        public int SoftwareTypeID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [DisplayName("Type")]
        public string Type { get; set; }


        public List<Software_Type> Software_Types { get; set; }

        /*
        public virtual ICollection<Software> Softwares { get; set; }
        
        public SoftwareType()
        {
            Softwares = new List<Software>();
        }
        */
    }
}
