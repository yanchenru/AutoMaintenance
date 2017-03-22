namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnMileage : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Vehicle", name: "Odometer", newName: "Mileage");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Vehicle", name: "Mileage", newName: "Odometer");
        }
    }
}
