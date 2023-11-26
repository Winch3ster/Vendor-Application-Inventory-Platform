using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public interface IEmployeeServices
    {

        Employee GetCurrentUser(string useremail);
        //Add employee in database
        Task AddAsync(Employee employee);

        Task<List<Employee>> GetAllAsync();

        //Update the actor data and return the updated result to user
        Task<Employee> UpdateAsync(int id, Employee newEmployeeData);

        Task<Employee> GetByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}
