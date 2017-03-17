namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "Make", c => c.String(maxLength: 60));
            AlterColumn("dbo.Vehicles", "Model", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Vehicles", "Rating", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Rating", c => c.String());
            AlterColumn("dbo.Vehicles", "Model", c => c.String());
            AlterColumn("dbo.Vehicles", "Make", c => c.String());
        }
    }
}
