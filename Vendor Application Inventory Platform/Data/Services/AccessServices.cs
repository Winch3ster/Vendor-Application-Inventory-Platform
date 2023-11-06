using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data.Services;

public class AccessServices:IAccessServices
{
    private readonly AppDbContext _db;
    public AccessServices(AppDbContext db)
    {
        _db = db;
    }
    
    public Employee? FindUserWithEmail(string email)
    {
        return _db.Employees.FirstOrDefault(e => e.Email == email);
    }

    public bool ComparePasswordsWithBCrypt(string inputPassword, string databasePassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, databasePassword);
    }
}