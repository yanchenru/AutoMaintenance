using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMaintenance.Models
{
    public class Maintenance
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }

    public class OilChange:Maintenance
    {
    }

    public class TireRotation:Maintenance
    {
    }
}