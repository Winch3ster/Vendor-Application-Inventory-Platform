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
    [Migration("20231101125436_addedEmployeeIdCol")]
    partial class addedEmployeeIdCol
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessAreaSoftware", b =>
                {
                    b.Property<int>("BusinessAreasBusinessAreaID")
                        .HasColumnType("int");

                    b.Property<int>("SoftwaresSoftwareID")
                        .HasColumnType("int");

                    b.HasKey("BusinessAreasBusinessAreaID", "SoftwaresSoftwareID");

                    b.HasIndex("SoftwaresSoftwareID");

                    b.ToTable("BusinessAreaSoftware");
                });

            modelBuilder.Entity("CompanyCountry", b =>
                {
                    b.Property<int>("CompaniesCompanyID")
                        .HasColumnType("int");

                    b.Property<int>("CountriesCountryID")
                        .HasColumnType("int");

                    b.HasKey("CompaniesCompanyID", "CountriesCountryID");

                    b.HasIndex("CountriesCountryID");

                    b.ToTable("CompanyCountry");
                });

            modelBuilder.Entity("SoftwareSoftwareModule", b =>
                {
                    b.Property<int>("SoftwareModulesSoftwareModuleID")
                        .HasColumnType("int");

                    b.Property<int>("SoftwaresSoftwareID")
                        .HasColumnType("int");

                    b.HasKey("SoftwareModulesSoftwareModuleID", "SoftwaresSoftwareID");

                    b.HasIndex("SoftwaresSoftwareID");

                    b.ToTable("SoftwareSoftwareModule");
                });

            modelBuilder.Entity("SoftwareSoftwareType", b =>
                {
                    b.Property<int>("SoftwareTypesSoftwareTypeID")
                        .HasColumnType("int");

                    b.Property<int>("SoftwaresSoftwareID")
                        .HasColumnType("int");

                    b.HasKey("SoftwareTypesSoftwareTypeID", "SoftwaresSoftwareID");

                    b.HasIndex("SoftwaresSoftwareID");

                    b.ToTable("SoftwareSoftwareType");
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

                    b.Property<int>("CityName")
                        .HasColumnType("int");

                    b.Property<int>("ContactID")
                        .HasColumnType("int");

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

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.ContactNumber", b =>
                {
                    b.Property<int>("ContactNumberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactNumberID"));

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

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

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<int>("Description")
                        .HasColumnType("int");

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

                    b.Property<string>("FinancialServicesClientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("BusinessAreaSoftware", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.BusinessArea", null)
                        .WithMany()
                        .HasForeignKey("BusinessAreasBusinessAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", null)
                        .WithMany()
                        .HasForeignKey("SoftwaresSoftwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyCountry", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompaniesCompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Country", null)
                        .WithMany()
                        .HasForeignKey("CountriesCountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SoftwareSoftwareModule", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.SoftwareModule", null)
                        .WithMany()
                        .HasForeignKey("SoftwareModulesSoftwareModuleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", null)
                        .WithMany()
                        .HasForeignKey("SoftwaresSoftwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SoftwareSoftwareType", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.SoftwareType", null)
                        .WithMany()
                        .HasForeignKey("SoftwareTypesSoftwareTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", null)
                        .WithMany()
                        .HasForeignKey("SoftwaresSoftwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.City", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.ContactNumber", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.City", "City")
                        .WithOne("ContactNumber")
                        .HasForeignKey("Vendor_Application_Inventory_Platform.Models.ContactNumber", "CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Review", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Employee", "Employee")
                        .WithMany("Reviews")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendor_Application_Inventory_Platform.Models.Software", "Software")
                        .WithMany("Reviews")
                        .HasForeignKey("SoftwareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Software");
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

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.City", b =>
                {
                    b.Navigation("ContactNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Company", b =>
                {
                    b.Navigation("Softwares");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Employee", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
