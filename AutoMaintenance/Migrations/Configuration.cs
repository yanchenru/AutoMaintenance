//namespace AutoMaintenance.Migrations
//{
//    using Models;
//    using System;
//    using System.Data.Entity;
//    using System.Data.Entity.Migrations;
//    using System.Linq;

//    internal sealed class Configuration : DbMigrationsConfiguration<AutoMaintenance.Models.VehicleDBContext>
//    {
//        public Configuration()
//        {
//            AutomaticMigrationsEnabled = false;
//        }

//        protected override void Seed(AutoMaintenance.Models.VehicleDBContext context)
//        {
//            //  This method will be called after migrating to the latest version.

//            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//            //  to avoid creating duplicate seed data. E.g.
//            //
//            //    context.People.AddOrUpdate(
//            //      p => p.FullName,
//            //      new Person { FullName = "Andrew Peters" },
//            //      new Person { FullName = "Brice Lambson" },
//            //      new Person { FullName = "Rowan Miller" }
//            //    );
//            //
//            context.Vehicle.AddOrUpdate(i => new { i.Make, i.Model},
//                new Vehicle
//                {
//                    Make = "BMW",
//                    Model = "X5",
//                    Year = 2014,
//                    Odometer = 55000,
//                    Rating = "Good"
//                },

//                new Vehicle
//                {
//                    Make = "Honda",
//                    Model = "Civic",
//                    Year = 2016,
//                    Odometer = 30000,
//                    Rating = "Bad"
//                },

//                new Vehicle
//                {
//                    Make = "Lincoln",
//                    Model = "MKX",
//                    Year = 2013,
//                    Odometer = 72000,
//                    Rating = "Good"
//                },

//                new Vehicle
//                {
//                    Make = "Buick",
//                    Model = "ENCORE",
//                    Year = 2016,
//                    Odometer = 11300,
//                    Rating = "Bad"
//                },

//                new Vehicle
//                {
//                    Make = "Honda",
//                    Model = "Accord",
//                    Year = 1999,
//                    Odometer = 174000,
//                    Rating = "Bad"
//                }
//                );
//        }
//    }
//}
