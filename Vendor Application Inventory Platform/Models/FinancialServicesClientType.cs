namespace Vendor_Application_Inventory_Platform.Models
{
    public class FinancialServicesClientType
    {
        public int FinancialServicesClientTypeID { get; set; }
        public string Description { get; set; }

        public List<Software_FinancialServicesClientType> Software_FinancialServicesClientTypes { get; set; }

    }
}
