using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;

namespace Vendor_Application_Inventory_Platform.Areas.User.Data.Services;

public interface IPdfGenerationService
{
    public byte[] GeneratePdf(SoftwareCompanyVM viewModel);
}