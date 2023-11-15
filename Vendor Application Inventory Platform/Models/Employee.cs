using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Emial is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        public bool IsAdmin { get; set; }

        public List<Review> reviews { get; set; }

        /*
        public ICollection<Review>? Reviews { get; set; }
        
        public Employee()
        {
            Reviews = new List<Review>();
        }
        */
    }
}
