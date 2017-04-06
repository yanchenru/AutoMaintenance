using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoMaintenance.Models
{
    public class VehicleType
    {
        public int ID { get; set; }
        
        public string Type { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}