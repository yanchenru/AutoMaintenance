using AutoMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMaintenance.ViewModels
{
    public class EmployeeIndexData
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Maintenance> Maintenances { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}