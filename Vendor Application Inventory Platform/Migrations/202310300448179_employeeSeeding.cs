namespace Vendor_Application_Inventory_Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeSeeding : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Review", "EmployeeID", "dbo.Employee");
            DropPrimaryKey("dbo.Review");
            DropPrimaryKey("dbo.Employee");
            AddColumn("dbo.Review", "ReviewID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Employee", "EmployeeID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Review", "ReviewID");
            AddPrimaryKey("dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Review", "EmployeeID", "dbo.Employee", "EmployeeID", cascadeDelete: true);
            DropColumn("dbo.Company", "ContactNumber");
            DropColumn("dbo.Review", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Company", "ContactNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.Review", "EmployeeID", "dbo.Employee");
            DropPrimaryKey("dbo.Employee");
            DropPrimaryKey("dbo.Review");
            AlterColumn("dbo.Employee", "EmployeeID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Review", "ReviewID");
            AddPrimaryKey("dbo.Employee", "EmployeeID");
            AddPrimaryKey("dbo.Review", "ID");
            AddForeignKey("dbo.Review", "EmployeeID", "dbo.Employee", "EmployeeID", cascadeDelete: true);
        }
    }
}
