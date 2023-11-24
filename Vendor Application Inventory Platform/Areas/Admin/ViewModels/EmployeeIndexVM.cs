using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels
{
    public class EmployeeIndexVM
    {
        public Employee signedInUser { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
