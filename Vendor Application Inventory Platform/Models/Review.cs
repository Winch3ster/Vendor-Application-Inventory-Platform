using System.ComponentModel.DataAnnotations;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class Review
    {
       
        [Key] public int ReviewID { get; set; }

        public int EmployeeID { get; set; }

        public string employeeFirstName { get; set; }

        public string employeeLastName { get; set; }
        public int SoftwareID { get; set; }


        //New 
        public int givenStar {  get; set; }



        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReviewDate { get; set; }

        
        public Employee employee { get; set; }

        public Software software { get; set; }
        

    }
}
