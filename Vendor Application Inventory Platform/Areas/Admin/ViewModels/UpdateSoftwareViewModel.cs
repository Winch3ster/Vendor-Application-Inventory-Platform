using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class UpdateSoftwareViewModel: CreateSoftwareViewModel
{
    public int companyId { get; set; }
    
    public PdfDocument? pdfDocument { get; set; }
    
    public Software? software { get; set; }
    public List<BusinessArea> LinkedBusinessAreas { get; set; }
    public List<SoftwareModule>  LinkedSoftwareModules { get; set; }
    public List<FinancialServicesClientType>  LinkedFinancialServicesClientTypes { get; set; }
    public List<SoftwareType>  LinkedSoftwareTypes { get; set; }
}