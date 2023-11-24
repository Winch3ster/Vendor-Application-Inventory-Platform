using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data.Services
{
    public interface IUserEmployeeServices
    {

        //Add employee in database
        Task AddAsync(Employee employee);

        Task<IEnumerable<Employee>> GetAllAsync();

        //Update the actor data and return the updated result to user
        Task<Employee> UpdateAsync(int id, Employee newEmployeeData);

        Task<Employee> GetByIdAsync(int id);

        public Task<Employee> GetCurrentUser(string useremail);
    }
}
