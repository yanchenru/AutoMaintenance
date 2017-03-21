namespace AutoMaintenance.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoMaintenance.DAL.AutoTrackContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutoMaintenance.DAL.AutoTrackContext context)
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle{Make = "BMW",Model = "X3",Year = 2014,Odometer = 55000,Rating = "Good"},
                new Vehicle{Make = "Honda",Model = "Civic",Year = 2016,Odometer = 30000,Rating = "Bad"},
                new Vehicle{Make = "Lincoln",Model = "MKX",Year = 2013,Odometer = 72000,Rating = "Good"},
                new Vehicle{Make = "Buick",Model = "ENCORE",Year = 2016,Odometer = 11300,Rating = "Bad"},
                new Vehicle{Make = "Honda",Model = "Accord",Year = 1999,Odometer = 174000,Rating = "Bad"}
             };
            vehicles.ForEach(v => context.Vehicle.AddOrUpdate(p => p.Model, v));
            context.SaveChanges();

            var maintenances = new List<Maintenance>
            {
                new Maintenance{VehicleID=1, Date=DateTime.Parse("2017-03-19"), Price=150,Type=MaintenanceType.Type1 },
                new Maintenance{VehicleID=2, Date=DateTime.Parse("2015-03-09"), Price=250,Type=MaintenanceType.Type4 },
                new Maintenance{VehicleID=1, Date=DateTime.Parse("2017-03-19"), Price=160,Type=MaintenanceType.Type2 },
                new Maintenance{VehicleID=4, Date=DateTime.Parse("2017-02-12"), Price=650,Type=MaintenanceType.Type3},
                new Maintenance{VehicleID=3, Date=DateTime.Parse("2017-09-03"), Price=950,Type=MaintenanceType.Type1 },
                new Maintenance{VehicleID=1, Date=DateTime.Parse("2017-03-18"), Price=1250,Type=MaintenanceType.Type4 },
                new Maintenance{VehicleID=4, Date=DateTime.Parse("2017-07-29"), Price=1650,Type=MaintenanceType.Type2 }
            };
            maintenances.ForEach(m => context.Maintenance.AddOrUpdate(m));
            context.SaveChanges();
        }
    }
}
