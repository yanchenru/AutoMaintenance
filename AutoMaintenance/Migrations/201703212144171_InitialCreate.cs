namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Maintenance",
                c => new
                    {
                        MaintenanceID = c.Int(nullable: false, identity: true),
                        VehicleID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Type = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MaintenanceID)
                .ForeignKey("dbo.Vehicle", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.VehicleID);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Make = c.String(maxLength: 60),
                        Model = c.String(nullable: false, maxLength: 30),
                        Year = c.Int(nullable: false),
                        Odometer = c.Int(nullable: false),
                        Rating = c.String(maxLength: 4),
                        GasType = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maintenance", "VehicleID", "dbo.Vehicle");
            DropIndex("dbo.Maintenance", new[] { "VehicleID" });
            DropTable("dbo.Vehicle");
            DropTable("dbo.Maintenance");
        }
    }
}
