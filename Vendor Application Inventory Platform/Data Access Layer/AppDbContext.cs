using Vendor_Application_Inventory_Platform.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;

namespace Vendor_Application_Inventory_Platform.Data_Access_Layer
{
    public class AppDbContext :DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Software> Softwares { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<SoftwareModule> SoftwareModules { get; set; }
        public DbSet<SoftwareType> SoftwareTypes { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<ContactNumber> ContactNumbers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //remove naming convenion so table names will be singular

        }
    }
}
