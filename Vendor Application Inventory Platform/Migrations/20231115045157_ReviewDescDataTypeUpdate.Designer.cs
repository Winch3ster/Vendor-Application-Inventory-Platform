﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231115045157_ReviewDescDataTypeUpdate")]
    partial class ReviewDescDataTypeUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("CityID")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.BusinessArea", b =>
                {
                    b.Property<int>("BusinessAreaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessAreaID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusinessAreaID");

                    b.ToTable("BusinessAreas");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityID"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.HasKey("CityID");

                    b.HasIndex("CountryID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EstablishedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("InternalProfessionalServices")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastDemoDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfEmployee")
                        .HasColumnType("int");

                    b.Property<string>("WebsiteURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Company_Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("companyID")
                        .HasColumnType("int");

                    b.Property<int>("countryID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("companyID");

                    b.HasIndex("countryID");

                    b.ToTable("Company_Country");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.ContactNumber", b =>
                {
                    b.Property<int>("ContactNumberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactNumberID"));

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<long>("Number")
                        .HasColumnType("bigint");

                    b.HasKey("ContactNumberID");

                    b.HasIndex("CityID")
                        .IsUnique();

                    b.ToTable("ContactNumbers");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryID"));

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.FinancialServicesClientType", b =>
                {
                    b.Property<int>("FinancialServicesClientTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FinancialServicesClientTypeID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FinancialServicesClientTypeID");

                    b.ToTable("FinancialServicesClientTypes");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoftwareID")
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("SoftwareID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software", b =>
                {
                    b.Property<int>("SoftwareID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareID"));

                    b.Property<int>("Cloud")
                        .HasColumnType("int");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DocumentAttached")
                        .HasColumnType("bit");

                    b.Property<string>("SoftwareName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SoftwareID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.SoftwareModule", b =>
                {
                    b.Property<int>("SoftwareModuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareModuleID"));

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SoftwareModuleID");

                    b.ToTable("SoftwareModules");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.SoftwareType", b =>
                {
                    b.Property<int>("SoftwareTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareTypeID"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SoftwareTypeID");

                    b.ToTable("SoftwareTypes");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("areaID")
                        .HasColumnType("int");

                    b.Property<int>("softwareID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("areaID");

                    b.HasIndex("softwareID");

                    b.ToTable("Software_Areas");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_FinancialServicesClientType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("financialServicesClientTypeID")
                        .HasColumnType("int");

                    b.Property<int>("softwareID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("financialServicesClientTypeID");

                    b.HasIndex("softwareID");

                    b.ToTable("Software_FinancialServicesClientTypes");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("moduleID")
                        .HasColumnType("int");

                    b.Property<int>("softwareID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("moduleID");

                    b.HasIndex("softwareID");

                    b.ToTable("Software_Modules");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("softwareID")
                        .HasColumnType("int");

                    b.Property<int>("typeID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("softwareID");

                    b.HasIndex("typeID");

                    b.ToTable("Software_Types");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Address", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.City", "city")
                        .WithOne("address")
                        .HasForeignKey("Vendor_Application_Inventory_Platform.Models.Address", "CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.City", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Country", "country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("country");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Company_Country", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Company", "company")
                        .WithMany("Company_Countries")
                        .HasForeignKey("companyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Country", "country")
                        .WithMany("Company_Countries")
                        .HasForeignKey("countryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("company");

                    b.Navigation("country");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.ContactNumber", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.City", "city")
                        .WithOne("contactNumber")
                        .HasForeignKey("Vendor_Application_Inventory_Platform.Models.ContactNumber", "CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Review", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Employee", "employee")
                        .WithMany("reviews")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", "software")
                        .WithMany("reviews")
                        .HasForeignKey("SoftwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");

                    b.Navigation("software");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Company", "Company")
                        .WithMany("Softwares")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_Area", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.BusinessArea", "businessArea")
                        .WithMany("Software_Areas")
                        .HasForeignKey("areaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", "software")
                        .WithMany("Software_Areas")
                        .HasForeignKey("softwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("businessArea");

                    b.Navigation("software");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_FinancialServicesClientType", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.FinancialServicesClientType", "financialServicesClientType")
                        .WithMany("Software_FinancialServicesClientTypes")
                        .HasForeignKey("financialServicesClientTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", "software")
                        .WithMany("Software_FinancialServicesClientTypes")
                        .HasForeignKey("softwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("financialServicesClientType");

                    b.Navigation("software");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_Module", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.SoftwareModule", "softwareModule")
                        .WithMany("Software_Modules")
                        .HasForeignKey("moduleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", "software")
                        .WithMany("Software_Modules")
                        .HasForeignKey("softwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("software");

                    b.Navigation("softwareModule");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software_Type", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", "software")
                        .WithMany("Software_Types")
                        .HasForeignKey("softwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.SoftwareType", "softwareType")
                        .WithMany("Software_Types")
                        .HasForeignKey("typeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("software");

                    b.Navigation("softwareType");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.BusinessArea", b =>
                {
                    b.Navigation("Software_Areas");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.City", b =>
                {
                    b.Navigation("address")
                        .IsRequired();

                    b.Navigation("contactNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Company", b =>
                {
                    b.Navigation("Company_Countries");

                    b.Navigation("Softwares");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Company_Countries");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Employee", b =>
                {
                    b.Navigation("reviews");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.FinancialServicesClientType", b =>
                {
                    b.Navigation("Software_FinancialServicesClientTypes");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software", b =>
                {
                    b.Navigation("Software_Areas");

                    b.Navigation("Software_FinancialServicesClientTypes");

                    b.Navigation("Software_Modules");

                    b.Navigation("Software_Types");

                    b.Navigation("reviews");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.SoftwareModule", b =>
                {
                    b.Navigation("Software_Modules");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.SoftwareType", b =>
                {
                    b.Navigation("Software_Types");
                });
#pragma warning restore 612, 618
        }
    }
}
