using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public interface ISoftwareServices
    {

        public List<Company?> ListAllCompanies();
        public List<BusinessArea> ListAllBusinessAreas();
        public List<SoftwareModule> ListAllModules();
        public List<FinancialServicesClientType> ListAllFinancialServicesClientTypes();
        public List<SoftwareType> ListAllSoftwareType();
        
        public List<Software> ListAllSoftware();

        public BusinessArea CreateBusinessArea(BusinessArea businessArea);
        
        public SoftwareType CreateSoftwareType(SoftwareType softwareType);

        public SoftwareModule CreateSoftwareModule(SoftwareModule softwareModule);

        public FinancialServicesClientType CreateFinancialService(FinancialServicesClientType financialServicesClientType);

        public Software CreateNewSoftware(Software software);

        public void Link_Software_Type(int softwareId, List<int> softwareTypeIds);
        public void Link_Software_Module(int softwareId, List<int> softwareModuleIds);
        public void Link_Software_Financial(int softwareId, List<int> financialIds);
        public void Link_Software_Area(int softwareId, List<int> areaIds);
        
        public List<BusinessArea> GetBusinessAreas(int softwareId);
        public List<SoftwareModule> GetModules(int softwareId);
        public List<FinancialServicesClientType> GetFinancialServicesClientTypes(int softwareId);
        public List<SoftwareType> GetSoftwareType(int softwareId);
        
        public List<string> GetBusinessAreaNames(int softwareId);
        public List<string> GetModuleNames(int softwareId);
        public List<string> GetFinancialServicesClientTypeNames(int softwareId);
        public List<string> GetSoftwareTypeNames(int softwareId);

        public Software? FindSoftware(int softwareId);
        
        public void UpdateSoftware(int softwareId, Software software);

        public void CreatePdf(PdfDocument pdfDocument);
        
        public  PdfDocument? GetPdf(int softwareId);
        
        public void deleteSoftware(int softwareId);


        public List<Software> GetSoftwareToBeReviewed();

        public Software GetSoftwareById(int softwareId);
        public void MarkAsReviewed(Software software);
        public void UpdateLastReviewDate(Software software);
    }
}
