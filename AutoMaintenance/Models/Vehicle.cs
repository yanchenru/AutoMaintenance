﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoMaintenance.Models
{
    public class Vehicle
    {
        public int ID { get; set; }

        public int VehicleTypeID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Make { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Model { get; set; }

        [Range(1900, int.MaxValue)]
        public int Year { get; set; }

        [Display(Name = "Mileage")]
        //[Column("Mileage")]
        public int Odometer { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(4)]
        public string Rating { get; set; }

        //public string Type
        //{
        //    get
        //    {
        //        return this.GetType().Name;
        //    }
        //}

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
    }

    //public class VehicleDBContext : DbContext
    //{
    //    public DbSet<Vehicle> Vehicle { get; set; }
    //}

    public class Gas : Vehicle
    {
        public string GasType { get; set; }
    }

    public class Electric : Vehicle
    {
        public bool? Charged { get; set; }
    }

    public class Diesel : Vehicle
    {

    }
}