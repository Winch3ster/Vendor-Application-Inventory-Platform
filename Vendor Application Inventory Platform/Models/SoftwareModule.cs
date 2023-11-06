using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class SoftwareModule
    {
        public int SoftwareModuleID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [DisplayName("Module")]
        public string Module { get; set; }


        public virtual ICollection<Software> Softwares { get; set; }
        
        
        public SoftwareModule()
        {
            Softwares = new List<Software>();
        }
    }
}
