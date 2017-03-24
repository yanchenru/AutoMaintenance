using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMaintenance.ViewModels
{
    public class AssignedEmployeeData
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool Assigned { get; set; }
    }
}