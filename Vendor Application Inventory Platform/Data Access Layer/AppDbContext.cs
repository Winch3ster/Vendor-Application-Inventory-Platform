using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Vendor_Application_Inventory_Platform.Models;

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
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<SoftwareModule> SoftwareModules { get; set; }
        public DbSet<SoftwareType> SoftwareTypes { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<ContactNumber> ContactNumbers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Many to many relationship between software and business area
            modelBuilder.Entity<Software>()
                .HasMany(software => software.BusinessAreas)
                .WithMany(businessArea => businessArea.Softwares);

            //Many to many relationship between software and modules
            modelBuilder.Entity<Software>()
                .HasMany(software => software.SoftwareModules)
                .WithMany(module => module.Softwares);

            //Many to many relationship between software and software type
            modelBuilder.Entity<Software>()
                .HasMany(software => software.SoftwareTypes)
                .WithMany(softwareType => softwareType.Softwares);

            //Many to many relationship between company and country
            modelBuilder.Entity<Company>()
                .HasMany(company => company.Countries)
                .WithMany(country => country.Companies);

            base.OnModelCreating(modelBuilder);

        }
    }
}
