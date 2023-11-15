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

        public DbSet<Company?> Companies { get; set; }
        public DbSet<Address?> Addresses { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<SoftwareModule> SoftwareModules { get; set; }
        public DbSet<SoftwareType> SoftwareTypes { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City?> Cities { get; set; }
        
        public DbSet<ContactNumber> ContactNumbers { get; set; }

        public DbSet<Software_Area> Software_Areas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Many to many relationship between software and business area
            //One to many relationship of software and Software_Area
            modelBuilder.Entity<Software_Area>()
                .HasOne(s => s.software)
                .WithMany(sa => sa.Software_Areas)
                .HasForeignKey(si => si.softwareID);
            //One to many relationship of BusinessArea and Software_Area
            modelBuilder.Entity<Software_Area>()
                .HasOne(a => a.businessArea)
                .WithMany(sa => sa.Software_Areas)
                .HasForeignKey(si => si.areaID);






        }
    }
}
