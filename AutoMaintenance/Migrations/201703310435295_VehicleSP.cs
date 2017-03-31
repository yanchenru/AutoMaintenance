namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
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
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
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
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Diesel_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Diesel_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
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
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Electric_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Electric_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
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
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Gas_Update",
                p => new
                    {
                        ID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        GasType = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating, [GasType] = @GasType
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Gas_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Vehicle]
                      WHERE ([ID] = @ID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Gas_Delete");
            DropStoredProcedure("dbo.Gas_Update");
            DropStoredProcedure("dbo.Gas_Insert");
            DropStoredProcedure("dbo.Electric_Delete");
            DropStoredProcedure("dbo.Electric_Update");
            DropStoredProcedure("dbo.Electric_Insert");
            DropStoredProcedure("dbo.Diesel_Delete");
            DropStoredProcedure("dbo.Diesel_Update");
            DropStoredProcedure("dbo.Diesel_Insert");
            DropStoredProcedure("dbo.Vehicle_Delete");
            DropStoredProcedure("dbo.Vehicle_Update");
            DropStoredProcedure("dbo.Vehicle_Insert");
        }
    }
}
