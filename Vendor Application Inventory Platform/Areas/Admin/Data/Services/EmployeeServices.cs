using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public class EmployeeServices : IEmployeeServices //Dealing with database
    {
        //Inject database to this class
        private AppDbContext _context;

        public EmployeeServices(AppDbContext c)
        {
            _context = c;
        }

        public async Task AddAsync(Employee employee)
        {
            employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
        }



        public async Task<Employee> GetCurrentUser(string useremail)
        {
            

            var result = await _context.Employees.FirstOrDefaultAsync(e => e.Email == useremail); //Set it to a generic method
            return result;
        }
       


        public async Task<List<Employee>> GetAllAsync()
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
            //check if password is null, if it is, then do not update password
            
            if (newEmployeerData.Password == null)
            {
                // Code to handle password null case
                var employee = await _context.Employees.FirstOrDefaultAsync(n => n.EmployeeID == newEmployeerData.EmployeeID);

                // Detach the entity if it is already being tracked
                _context.Entry(employee).State = EntityState.Detached;

                // Make changes to the detached entity
                newEmployeerData.Password = employee.Password;

                // Attach the modified entity back to the context
                _context.Update(newEmployeerData);

                // Save changes
                await _context.SaveChangesAsync();
            }
            else
            {
                // Code to handle password not null case
                newEmployeerData.Password = BCrypt.Net.BCrypt.HashPassword(newEmployeerData.Password);

                // Update the entity
                _context.Update(newEmployeerData);

                // Save changes
                await _context.SaveChangesAsync();
            }

            
            return newEmployeerData;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(n => n.EmployeeID == id);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

        }

        
    }
}
