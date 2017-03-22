namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeMaintenance",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        MaintenanceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.MaintenanceID })
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Maintenance", t => t.MaintenanceID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.MaintenanceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeMaintenance", "MaintenanceID", "dbo.Maintenance");
            DropForeignKey("dbo.EmployeeMaintenance", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.EmployeeMaintenance", new[] { "MaintenanceID" });
            DropIndex("dbo.EmployeeMaintenance", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeMaintenance");
            DropTable("dbo.Employee");
        }
    }
}
