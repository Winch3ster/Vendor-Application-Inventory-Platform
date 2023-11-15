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
    [Migration("20231112152152_AddedDbSetForSoftwareArea")]
    partial class AddedDbSetForSoftwareArea
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

                    b.HasKey("AddressID");

                    b.HasIndex("CityID");

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

                    b.HasIndex("CityID");

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

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Address", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.City", "city")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.ContactNumber", b =>
                {
                    b.HasOne("Vendor_Application_Inventory_Platform.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
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

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.BusinessArea", b =>
                {
                    b.Navigation("Software_Areas");
                });

            modelBuilder.Entity("Vendor_Application_Inventory_Platform.Models.Software", b =>
                {
                    b.Navigation("Software_Areas");
                });
#pragma warning restore 612, 618
        }
    }
}
