namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicle", "Charged", c => c.Boolean());
            AlterStoredProcedure(
                "dbo.Electric_Insert",
                p => new
                    {
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        Charged = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([Make], [Model], [Year], [Mileage], [Rating], [Charged], [GasType], [Discriminator])
                      VALUES (@Make, @Model, @Year, @Mileage, @Rating, @Charged, NULL, N'Electric')
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Vehicle]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID], t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Electric_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                        Charged = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating, [Charged] = @Charged
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicle", "Charged");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
