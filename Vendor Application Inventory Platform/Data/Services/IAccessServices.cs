using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data.Services;

public interface IAccessServices
{
    Employee? FindUserWithEmail(string email);

    bool ComparePasswordsWithBCrypt(string inputPassword, string databasePassword);
}