namespace AutoMaintenance.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoMaintenance.Models.VehicleDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutoMaintenance.Models.VehicleDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Vehicle.AddOrUpdate(i => i.Make,
        new Vehicle
        {
            Make = "BMW",
            Model = "X5",
            Year = 2014,
            Odometer = 55000
        },

         new Vehicle
         {
             Make = "Honda",
             Model = "Civic",
             Year = 2016,
             Odometer = 30000
         }
   );
        }
    }
}
