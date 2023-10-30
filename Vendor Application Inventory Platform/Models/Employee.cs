﻿namespace Vendor_Application_Inventory_Platform.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }


        public ICollection<Review> Reviews { get; set; }
    }
}
