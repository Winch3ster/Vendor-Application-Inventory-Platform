using Microsoft.EntityFrameworkCore;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data.Services
{
    public class EmployeeServices : IUserEmployeeServices //Dealing with database
    {
        //Inject database to this class
        private AppDbContext _context;

        public EmployeeServices(AppDbContext c)
        {
            _context = c;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
        }




        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var results = await _context.Employees.ToListAsync(); //Set it to a generic method
            return results;
        }





        public async Task<Employee> GetByIdAsync(int id)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(n => n.EmployeeID == id);
            return result;
        }

        public async Task<Employee> UpdateAsync(int id, Employee newEmployeerData)
        {
            _context.Update(newEmployeerData);
            await _context.SaveChangesAsync();
            return newEmployeerData;
        }



    }
}
