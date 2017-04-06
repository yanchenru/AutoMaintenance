namespace AutoMaintenance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicleType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Vehicle", "VehicleTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicle", "VehicleTypeID");
            AddForeignKey("dbo.Vehicle", "VehicleTypeID", "dbo.VehicleType", "ID", cascadeDelete: true);
            AlterStoredProcedure(
                "dbo.Vehicle_Insert",
                p => new
                    {
                        VehicleTypeID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([VehicleTypeID], [Make], [Model], [Year], [Mileage], [Rating], [Charged], [GasType], [Discriminator])
                      VALUES (@VehicleTypeID, @Make, @Model, @Year, @Mileage, @Rating, NULL, NULL, N'Vehicle')
                      
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
                        VehicleTypeID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [VehicleTypeID] = @VehicleTypeID, [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Diesel_Insert",
                p => new
                    {
                        VehicleTypeID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([VehicleTypeID], [Make], [Model], [Year], [Mileage], [Rating], [Charged], [GasType], [Discriminator])
                      VALUES (@VehicleTypeID, @Make, @Model, @Year, @Mileage, @Rating, NULL, NULL, N'Diesel')
                      
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
                        VehicleTypeID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Vehicle]
                      SET [VehicleTypeID] = @VehicleTypeID, [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Electric_Insert",
                p => new
                    {
                        VehicleTypeID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        Charged = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([VehicleTypeID], [Make], [Model], [Year], [Mileage], [Rating], [Charged], [GasType], [Discriminator])
                      VALUES (@VehicleTypeID, @Make, @Model, @Year, @Mileage, @Rating, @Charged, NULL, N'Electric')
                      
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
                        VehicleTypeID = p.Int(),
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
                      SET [VehicleTypeID] = @VehicleTypeID, [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating, [Charged] = @Charged
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.Gas_Insert",
                p => new
                    {
                        VehicleTypeID = p.Int(),
                        Make = p.String(maxLength: 60),
                        Model = p.String(maxLength: 30),
                        Year = p.Int(),
                        Mileage = p.Int(),
                        Rating = p.String(maxLength: 4),
                        GasType = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Vehicle]([VehicleTypeID], [Make], [Model], [Year], [Mileage], [Rating], [Charged], [GasType], [Discriminator])
                      VALUES (@VehicleTypeID, @Make, @Model, @Year, @Mileage, @Rating, NULL, @GasType, N'Gas')
                      
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
                        VehicleTypeID = p.Int(),
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
                      SET [VehicleTypeID] = @VehicleTypeID, [Make] = @Make, [Model] = @Model, [Year] = @Year, [Mileage] = @Mileage, [Rating] = @Rating, [GasType] = @GasType
                      WHERE (([ID] = @ID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Vehicle] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicle", "VehicleTypeID", "dbo.VehicleType");
            DropIndex("dbo.Vehicle", new[] { "VehicleTypeID" });
            DropColumn("dbo.Vehicle", "VehicleTypeID");
            DropTable("dbo.VehicleType");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
