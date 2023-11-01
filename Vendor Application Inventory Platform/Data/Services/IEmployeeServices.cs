using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data.Services
{
    public interface IEmployeeServices
    {

        //Add employee in database
        Task AddAsync(Employee employee);

        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
