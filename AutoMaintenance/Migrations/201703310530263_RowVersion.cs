namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicle", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterStoredProcedure(
                "dbo.Vehicle_Insert",
                p => new
                    {
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([Make], [Model], [Year], [Mileage], [Rating], [GasType], [Discriminator])
                      VALUES (@Make, @Model, @Year, @Mileage, @Rating, NULL, N'Vehicle')
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Vehicle]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID], t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Vehicle_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Vehicle_Delete",
                p => new
                    {
                        ID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
            AlterStoredProcedure(
                "dbo.Diesel_Insert",
                p => new
                    {
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([Make], [Model], [Year], [Mileage], [Rating], [GasType], [Discriminator])
                      VALUES (@Make, @Model, @Year, @Mileage, @Rating, NULL, N'Diesel')
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Vehicle]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID], t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Diesel_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Diesel_Delete",
                p => new
                    {
                        ID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
            AlterStoredProcedure(
                "dbo.Electric_Insert",
                p => new
                    {
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([Make], [Model], [Year], [Mileage], [Rating], [GasType], [Discriminator])
                      VALUES (@Make, @Model, @Year, @Mileage, @Rating, NULL, N'Electric')
                      
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
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Electric_Delete",
                p => new
                    {
                        ID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
            AlterStoredProcedure(
                "dbo.Gas_Insert",
                p => new
                    {
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        GasType = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([Make], [Model], [Year], [Mileage], [Rating], [GasType], [Discriminator])
                      VALUES (@Make, @Model, @Year, @Mileage, @Rating, @GasType, N'Gas')
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Vehicle]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID], t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Gas_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                        GasType = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating, [GasType] = @GasType
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Gas_Delete",
                p => new
                    {
                        ID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicle", "RowVersion");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
