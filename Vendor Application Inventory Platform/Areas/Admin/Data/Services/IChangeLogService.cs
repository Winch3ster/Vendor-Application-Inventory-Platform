using Vendor_Application_Inventory_Platform.Data.Enum;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public interface IChangeLogService
    {
        void AddChange(string entitiyName, Actions action);
        void AddChangeDeleteSoftwareById(int id, Actions action);
        void AddChangeDeleteCompanyById(int id, Actions action);

    }

}
