using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoMaintenance.Models
{
    public class Maintenance
    {
        public int ID { get; set; }

        //[Display(Name = "Maintenance Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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