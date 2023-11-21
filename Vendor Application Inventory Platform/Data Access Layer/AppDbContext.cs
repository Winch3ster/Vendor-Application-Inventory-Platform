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
        
        public DbSet<PdfDocument> PdfDocuments { get; set; }
        public DbSet<Company?> Companies { get; set; }
        public DbSet<Address?> Addresses { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<SoftwareModule> SoftwareModules { get; set; }
        public DbSet<SoftwareType> SoftwareTypes { get; set; }
        public DbSet<FinancialServicesClientType> FinancialServicesClientTypes { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City?> Cities { get; set; }
        
        public DbSet<ContactNumber> ContactNumbers { get; set; }


        //Join tables
        public DbSet<Software_Area> Software_Areas { get; set; }

        public DbSet<Software_Module> Software_Modules { get; set; }
        public DbSet<Software_Type> Software_Types { get; set; }

        public DbSet<Software_FinancialServicesClientType> Software_FinancialServicesClientTypes { get; set; }
        public DbSet<Company_Country> Company_Country { get; set; }

        //Audit table (To track which user viewed which software)
        //This is for "recently viewed" implementation
        public DbSet<User_ViewHistory> user_ViewHistories { get; set; }


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



            //Many to many relationship between software and software module
            //One to many relationship of software and Software_Module
            modelBuilder.Entity<Software_Module>()
                .HasOne(s => s.software)
                .WithMany(sm => sm.Software_Modules)
                .HasForeignKey(si => si.softwareID);

            //One to many relationship of SoftwareModule and Software_Module
            modelBuilder.Entity<Software_Module>()
                .HasOne(m => m.softwareModule)
                .WithMany(sm => sm.Software_Modules)
                .HasForeignKey(si => si.moduleID);
            
            modelBuilder.Entity<FinancialServicesClientType>().ToTable("FinancialServicesClientType");





            //Many to many relationship between software and software type
            //One to many relationship of software and Software_Type
            modelBuilder.Entity<Software_Type>()
                .HasOne(s => s.software)
                .WithMany(sm => sm.Software_Types)
                .HasForeignKey(si => si.softwareID);

            //One to many relationship of SoftwareType and Software_Type
            modelBuilder.Entity<Software_Type>()
                .HasOne(m => m.softwareType)
                .WithMany(sm => sm.Software_Types)
                .HasForeignKey(si => si.typeID);


            //Many to many relationship between software and financial services client type
            //One to many relationship of software and Software_FinancialServicesClientType
            modelBuilder.Entity<Software_FinancialServicesClientType>()
                .HasOne(s => s.software)
                .WithMany(sf => sf.Software_FinancialServicesClientTypes)
                .HasForeignKey(si => si.softwareID);

            //One to many relationship of FinancialServicesClientType and Software_FinancialServicesClientType
            modelBuilder.Entity<Software_FinancialServicesClientType>()
                .HasOne(f => f.financialServicesClientType)
                .WithMany(sf => sf.Software_FinancialServicesClientTypes)
                .HasForeignKey(fi => fi.financialServicesClientTypeID);



            //Many to many relationship between company and country
            //One to many relationship of company and Company_Country
            modelBuilder.Entity<Company_Country>()
                .HasOne(comp => comp.company)
                .WithMany(companyCompany => companyCompany.Company_Countries)
                .HasForeignKey(compid => compid.companyID);

            //One to many relationship of Country and Company_Country
            modelBuilder.Entity<Company_Country>()
                .HasOne(coun => coun.country)
                .WithMany(companyCompany => companyCompany.Company_Countries)
                .HasForeignKey(counid => counid.countryID);


            //One to may relationship between company and software
            modelBuilder.Entity<Software>()
                .HasKey(b => b.SoftwareID);

            modelBuilder.Entity<Software>()
                .HasOne(s => s.Company)
                .WithMany(c => c.Softwares)
                .HasForeignKey(c => c.CompanyID);
            




            // Configure the one-to-many relationship between Country and Cities
            modelBuilder.Entity<City>()
                .HasOne(c => c.country)
                .WithMany(country => country.Cities)
                .HasForeignKey(c => c.CountryID)
                .OnDelete(DeleteBehavior.Cascade); //Delete the cities associated when country is deleted


            // Configure the one-to-many relationship between review and software
            modelBuilder.Entity<Review>()
                .HasOne(r => r.software)
                .WithMany(s => s.reviews)
                .HasForeignKey(r => r.SoftwareID)
                .OnDelete(DeleteBehavior.Cascade); //When software is deleted, delete the review also 



            // Configure the one-to-many relationship between user and review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.employee) //One review has one user
                .WithMany(e => e.reviews) //One user has many reviews
                .HasForeignKey(r => r.EmployeeID) //The review has user foreign key
                .OnDelete(DeleteBehavior.Cascade); //When employee is deleted, delete the review also 



            //Configure one to many relationship between user and User_ViewHistory
            modelBuilder.Entity<User_ViewHistory>()
                .HasOne(u_v  => u_v.U_V_Employee)
                .WithMany(u_v => u_v.user_ViewHistories)
                .HasForeignKey(u_v => u_v.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); //When user is deleted, delete its view histories

            modelBuilder.Entity<User_ViewHistory>()
               .HasOne(u_v => u_v.U_V_Software)
               .WithMany(u_v => u_v.user_ViewHistories)
               .HasForeignKey(u_v => u_v.SoftwareId)
               .OnDelete(DeleteBehavior.Cascade); //When software is deleted, delete its view histories

        }
    }
}
