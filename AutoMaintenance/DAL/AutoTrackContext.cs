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
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Employee>()
             .HasMany(e => e.Maintenances).WithMany(m => m.Employees)
             .Map(t => t.MapLeftKey("EmployeeID")
                 .MapRightKey("MaintenanceID")
                 .ToTable("EmployeeMaintenance"));

            modelBuilder.Entity<Vehicle>().MapToStoredProcedures();
        }
    }
}