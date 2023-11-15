namespace Vendor_Application_Inventory_Platform.Models
{
    public class Software_FinancialServicesClientType
    {
        public int Id { get; set; }

        public int financialServicesClientTypeID { get; set; }
        public FinancialServicesClientType financialServicesClientType { get; set; }


        public int softwareID { get; set; }
        public Software software { get; set; }

    }
}
