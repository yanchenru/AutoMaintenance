namespace AutoMaintenance.Migrations
{
    using DAL;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoTrackContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutoTrackContext context)
        {
            var vehicleTypes = new List<VehicleType>
            {
                new VehicleType{Type = "Gas", Vehicles=new List<Vehicle>()},
                new VehicleType{Type = "Electric", Vehicles=new List<Vehicle>()},
                new VehicleType{Type = "Diesel", Vehicles=new List<Vehicle>()},
            };
            vehicleTypes.ForEach(vt => context.VehicleType.AddOrUpdate(p => p.Type, vt));
            context.SaveChanges();

            var vehicles = new List<Vehicle>
            {
                new Vehicle{VehicleTypeID=1,Make = "BMW",Model = "X3",Year = 2014,Odometer = 55000,Rating = "Good"},
                new Vehicle{VehicleTypeID=1,Make = "Honda",Model = "Civic",Year = 2016,Odometer = 30000,Rating = "Bad"},
                new Vehicle{VehicleTypeID=3,Make = "Lincoln",Model = "MKX",Year = 2013,Odometer = 72000,Rating = "Good"},
                new Vehicle{VehicleTypeID=1,Make = "Buick",Model = "ENCORE",Year = 2016,Odometer = 11300,Rating = "Bad"},
                new Vehicle{VehicleTypeID=3,Make = "Honda",Model = "Accord",Year = 1999,Odometer = 174000,Rating = "Bad"},
                new Vehicle{VehicleTypeID=2,Make = "Tesla",Model = "Model 3",Year = 2017,Odometer = 1200,Rating = "Bad"}
             };
            vehicles.ForEach(v => context.Vehicle.AddOrUpdate(p => p.Model, v));
            context.SaveChanges();

            var maintenances = new List<Maintenance>
            {
                new Maintenance{VehicleID=1, Date=DateTime.Parse("2017-03-19"), Price=150,
                    Task =MaintenanceTask.OilChange, Employees=new List<Employee>()},
                new Maintenance{VehicleID=2, Date=DateTime.Parse("2015-03-09"), Price=250,
                    Task =MaintenanceTask.TireRotation, Employees=new List<Employee>() },
                new Maintenance{VehicleID=3, Date=DateTime.Parse("2017-03-19"), Price=160,
                    Task =MaintenanceTask.Vacuum, Employees=new List<Employee>() },
                new Maintenance{VehicleID=4, Date=DateTime.Parse("2016-02-12"), Price=650,
                    Task =MaintenanceTask.WheelAlignment, Employees=new List<Employee>()},
                new Maintenance{VehicleID=5, Date=DateTime.Parse("2015-09-03"), Price=950,
                    Task =MaintenanceTask.OilChange, Employees=new List<Employee>() },
                new Maintenance{VehicleID=1, Date=DateTime.Parse("2017-03-18"), Price=1250,
                    Task =MaintenanceTask.TireRotation, Employees=new List<Employee>() },
                new Maintenance{VehicleID=2, Date=DateTime.Parse("2016-07-29"), Price=1650,
                    Task =MaintenanceTask.Vacuum, Employees=new List<Employee>() }
            };
            maintenances.ForEach(m => context.Maintenance.AddOrUpdate(p =>new { p.VehicleID, p.Price}, m));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee { FirstMidName = "Kim", LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Employee { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Employee { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Employee { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Employee { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            employees.ForEach(e => context.Employee.AddOrUpdate(p => p.LastName, e));
            context.SaveChanges();

            AddOrUpdateEmployee(context, 1, "Kapoor");
            AddOrUpdateEmployee(context, 2, "Harui");
            AddOrUpdateEmployee(context, 3, "Zheng");
            AddOrUpdateEmployee(context, 4, "Zheng");
            AddOrUpdateEmployee(context, 5, "Fakhouri");
            AddOrUpdateEmployee(context, 6, "Harui");
            AddOrUpdateEmployee(context, 2, "Abercrombie");
            AddOrUpdateEmployee(context, 3, "Abercrombie");
        }

        private void AddOrUpdateEmployee(AutoTrackContext context, int maintenanceID, string employeeName)
        {
            var maintenance = context.Maintenance.SingleOrDefault(m => m.MaintenanceID== maintenanceID);
            var employee = maintenance.Employees.SingleOrDefault(e => e.LastName == employeeName);
            if (employee == null)
                maintenance.Employees.Add(context.Employee.Single(e => e.LastName == employeeName));
        }
    }
}
