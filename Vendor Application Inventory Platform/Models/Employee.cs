namespace Vendor_Application_Inventory_Platform.Models
{
    public class Employee
    {
        public int id { get; set; }
        public int employeeId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string employeeEmail { get; set; }

        public string password { get; set; }

        public bool isAdmin { get; set; }
    }
}
