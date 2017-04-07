namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaintenanceTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maintenance", "Task", c => c.Int());
            DropColumn("dbo.Maintenance", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Maintenance", "Type", c => c.Int());
            DropColumn("dbo.Maintenance", "Task");
        }
    }
}
