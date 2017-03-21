using AutoMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AutoMaintenance.DAL
{
    public class AutoTrackContext : DbContext
    {
        public AutoTrackContext() : base("AutoTrackContext")
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}