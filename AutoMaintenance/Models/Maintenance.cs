﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoMaintenance.Models
{
    public enum MaintenanceTask
    {
        OilChange,
        TireRotation,
        Vacuum,
        WheelAlignment
    }

    public class Maintenance
    {
        //public int ID { get; set; }
        public int MaintenanceID { get; set; }

        public int VehicleID { get; set; }

        [Display(Name = "Maintenance Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DisplayFormat(NullDisplayText = "No Type")]
        public MaintenanceTask? Task { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }

    public class OilChange : Maintenance
    {
    }

    public class TireRotation : Maintenance
    {
    }
}