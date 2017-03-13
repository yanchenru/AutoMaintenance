using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoMaintenance.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Odometer { get; set; }
        //public ICollection<Maintenance> Maintenance { get; set; }
    }

    public class VehicleDBContext : DbContext
    {
        public DbSet<Vehicle> Vehicle { get; set; }
    }

    public class Gas : Vehicle
    {
        public string GasType { get; set; }
    }

    public class Electric : Vehicle
    {

    }

    public class Diesel : Vehicle
    {

    }
}