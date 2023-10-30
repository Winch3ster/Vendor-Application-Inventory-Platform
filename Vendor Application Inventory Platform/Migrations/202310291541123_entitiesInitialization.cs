namespace Vendor_Application_Inventory_Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitiesInitialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessArea",
                c => new
                    {
                        BusinessAreaID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.BusinessAreaID);
            
            CreateTable(
                "dbo.Software",
                c => new
                    {
                        SoftwareID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        SoftwareName = c.String(),
                        Description = c.String(),
                        FinancialServicesClientType = c.String(),
                        Cloud = c.Int(nullable: false),
                        DocumentAttached = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SoftwareID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        WebsiteURL = c.String(),
                        Description = c.String(),
                        EstablishedDate = c.DateTime(nullable: false),
                        ContactNumber = c.Int(nullable: false),
                        NumberOfEmployee = c.Int(nullable: false),
                        InternalProfessionalServices = c.Boolean(nullable: false),
                        LastDemoDate = c.DateTime(nullable: false),
                        LastReviewDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CountryID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        CityName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.ContactNumber",
                c => new
                    {
                        ContactNumberID = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactNumberID)
                .ForeignKey("dbo.City", t => t.ContactNumberID)
                .Index(t => t.ContactNumberID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        SoftwareID = c.Int(nullable: false),
                        Description = c.Int(nullable: false),
                        ReviewDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Software", t => t.SoftwareID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.SoftwareID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.SoftwareModule",
                c => new
                    {
                        SoftwareModuleID = c.Int(nullable: false, identity: true),
                        Module = c.String(),
                    })
                .PrimaryKey(t => t.SoftwareModuleID);
            
            CreateTable(
                "dbo.SoftwareType",
                c => new
                    {
                        SoftwareTypeID = c.Int(nullable: false, identity: true),
                        SoftwareID = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.SoftwareTypeID);
            
            CreateTable(
                "dbo.Software_Area",
                c => new
                    {
                        SoftwareID = c.Int(nullable: false),
                        BusinessAreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SoftwareID, t.BusinessAreaID })
                .ForeignKey("dbo.Software", t => t.SoftwareID, cascadeDelete: true)
                .ForeignKey("dbo.BusinessArea", t => t.BusinessAreaID, cascadeDelete: true)
                .Index(t => t.SoftwareID)
                .Index(t => t.BusinessAreaID);
            
            CreateTable(
                "dbo.Company_Country",
                c => new
                    {
                        CompanyID = c.Int(nullable: false),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompanyID, t.CountryID })
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Software_Module",
                c => new
                    {
                        SoftwareID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SoftwareID, t.ModuleID })
                .ForeignKey("dbo.SoftwareModule", t => t.SoftwareID, cascadeDelete: true)
                .ForeignKey("dbo.Software", t => t.ModuleID, cascadeDelete: true)
                .Index(t => t.SoftwareID)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.Type_Software",
                c => new
                    {
                        SoftwareID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SoftwareID, t.TypeID })
                .ForeignKey("dbo.SoftwareType", t => t.SoftwareID, cascadeDelete: true)
                .ForeignKey("dbo.Software", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.SoftwareID)
                .Index(t => t.TypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Type_Software", "TypeID", "dbo.Software");
            DropForeignKey("dbo.Type_Software", "SoftwareID", "dbo.SoftwareType");
            DropForeignKey("dbo.Software_Module", "ModuleID", "dbo.Software");
            DropForeignKey("dbo.Software_Module", "SoftwareID", "dbo.SoftwareModule");
            DropForeignKey("dbo.Review", "SoftwareID", "dbo.Software");
            DropForeignKey("dbo.Review", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Software", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Company_Country", "CountryID", "dbo.Country");
            DropForeignKey("dbo.Company_Country", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.City", "CountryID", "dbo.Country");
            DropForeignKey("dbo.ContactNumber", "ContactNumberID", "dbo.City");
            DropForeignKey("dbo.Software_Area", "BusinessAreaID", "dbo.BusinessArea");
            DropForeignKey("dbo.Software_Area", "SoftwareID", "dbo.Software");
            DropIndex("dbo.Type_Software", new[] { "TypeID" });
            DropIndex("dbo.Type_Software", new[] { "SoftwareID" });
            DropIndex("dbo.Software_Module", new[] { "ModuleID" });
            DropIndex("dbo.Software_Module", new[] { "SoftwareID" });
            DropIndex("dbo.Company_Country", new[] { "CountryID" });
            DropIndex("dbo.Company_Country", new[] { "CompanyID" });
            DropIndex("dbo.Software_Area", new[] { "BusinessAreaID" });
            DropIndex("dbo.Software_Area", new[] { "SoftwareID" });
            DropIndex("dbo.Review", new[] { "SoftwareID" });
            DropIndex("dbo.Review", new[] { "EmployeeID" });
            DropIndex("dbo.ContactNumber", new[] { "ContactNumberID" });
            DropIndex("dbo.City", new[] { "CountryID" });
            DropIndex("dbo.Software", new[] { "CompanyID" });
            DropTable("dbo.Type_Software");
            DropTable("dbo.Software_Module");
            DropTable("dbo.Company_Country");
            DropTable("dbo.Software_Area");
            DropTable("dbo.SoftwareType");
            DropTable("dbo.SoftwareModule");
            DropTable("dbo.Employee");
            DropTable("dbo.Review");
            DropTable("dbo.ContactNumber");
            DropTable("dbo.City");
            DropTable("dbo.Country");
            DropTable("dbo.Company");
            DropTable("dbo.Software");
            DropTable("dbo.BusinessArea");
        }
    }
}
