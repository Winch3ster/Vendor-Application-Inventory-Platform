using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public bool accountAccess { get; set; }
        
        public bool softwareAccess { get; set; }
        
        public bool companyAccess { get; set; }

        public List<Review> reviews { get; set; }
        
    }
}
