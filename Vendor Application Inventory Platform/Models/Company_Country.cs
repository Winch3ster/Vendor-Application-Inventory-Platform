namespace Vendor_Application_Inventory_Platform.Models
{
    public class Company_Country
    {

        public int Id { get; set; }

        public int companyID { get; set; }
        public Company company { get; set; }


        public int countryID { get; set; }
        public Country country { get; set; }
    }
}
