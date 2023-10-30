using Vendor_Application_Inventory_Platform.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Vendor_Application_Inventory_Platform.Data_Access_Layer
{
    public class AppDbContext :DbContext
    {

        public AppDbContext() : base("AppDbContext")
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




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            //remove naming convenion so table names will be singular
            //Eg. Employee table will stays as Employee instead of Employees
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 


            //Configure relationships among entities


            //Many-to-many relationship between Software and Business Area
            modelBuilder.Entity<Software>()
                .HasMany(software => software.BusinessAreas).WithMany(businessArea => businessArea.Softwares)
                .Map(t => t.MapLeftKey("SoftwareID")
                .MapRightKey("BusinessAreaID")
                .ToTable("Software_Area"));



            //Many-to-many relationship between Software and Modules
            modelBuilder.Entity<SoftwareModule>()
                .HasMany(module => module.Softwares).WithMany(software => software.SoftwareModules)
                .Map(t => t.MapLeftKey("SoftwareID")
                .MapRightKey("ModuleID")
                .ToTable("Software_Module"));


            //Many-to-many relationship between Software and Software types
            modelBuilder.Entity<SoftwareType>()
                .HasMany(type => type.Softwares).WithMany(software => software.SoftwareTypes)
                .Map(t => t.MapLeftKey("SoftwareID")
                .MapRightKey("TypeID")
                .ToTable("Type_Software"));



            //Many-to-many relationship between Company and Country
            modelBuilder.Entity<Company>()
                .HasMany(company => company.Countries).WithMany(country => country.Companies)
                .Map(t => t.MapLeftKey("CompanyID")
                .MapRightKey("CountryID")
                .ToTable("Company_Country"));

            

            modelBuilder.Entity<City>()
                .HasOptional(c => c.ContactNumber) // City can have zero or one ContactNumber
                .WithRequired(cn => cn.City); // ContactNumber is required and has one City


        }

    }
}
