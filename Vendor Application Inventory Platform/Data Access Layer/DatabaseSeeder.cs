using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data_Access_Layer
{
    public class DatabaseSeeder
    {
  
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Reference to appdbcontext file. This file is used to send and get data from the database

                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                //context is like an instance of the database
                context.Database.EnsureCreated();

                //Check if employee table is empty
                //if the table is empty, add employee data
                if(context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee() {

                            FirstName="John",
                            LastName="William",
                            Email = "John@example.com",
                            Password = "123",
                            IsAdmin=true,
                        },
                        new Employee() {

                            FirstName = "Henrich",
                            LastName = "Eddinburg",
                            Email = "Henrich@example.com",
                            Password = "Henrich123",
                            IsAdmin = false,
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
