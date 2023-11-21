using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class User_ViewHistory
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee U_V_Employee { get; set; }
        public int SoftwareId { get; set; }
        public Software U_V_Software { get; set; }
        public DateTime time { get; set; }
    }
}
