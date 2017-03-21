using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoMaintenance.ViewModels
{
    public class MaintenanceDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? MaintenanceDate { get; set; }

        public int MaintenanceCount { get; set; }    
    }
}