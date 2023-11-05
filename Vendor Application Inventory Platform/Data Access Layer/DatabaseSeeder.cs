using System.Security.Cryptography;
using System.Text;
using Vendor_Application_Inventory_Platform.Models;
using BCrypt.Net;

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

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("abc123");

                //Check if employee table is empty

                //if the table is empty, add employee data

                var employeesToAdd = new List<Employee>();


                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee()
                        {
                            FirstName = "John",
                            LastName = "William",
                            Email = "John@example.com",
                            Password = hashedPassword,
                            IsAdmin = true,
                        },
                        new Employee()
                        {
                            FirstName = "Henrich",
                            LastName = "Eddinburg",
                            Email = "Henrich@example.com",
                            Password = hashedPassword,
                            IsAdmin = false,
                        }
                    });

                    context.SaveChanges();





                
            }

        }
    }
}
