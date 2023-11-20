using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;

namespace Vendor_Application_Inventory_Platform.Areas.User.Data.Services
{

    public interface IExcelGenerationService
    {
        Task<byte[]> GenerateExcelAsync(SoftwareCompanyVM softwareCompanyVM);
    }

}
