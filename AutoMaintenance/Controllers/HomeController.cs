using AutoMaintenance.DAL;
using AutoMaintenance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoMaintenance.Controllers
{
    public class HomeController : Controller
    {
        private AutoTrackContext db = new AutoTrackContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<MaintenanceDateGroup> data = from m in db.Maintenance
                                                    group m by m.Date into dateGroup
                                                    select new MaintenanceDateGroup()
                                                    {
                                                        MaintenanceDate = dateGroup.Key,
                                                        MaintenanceCount = dateGroup.Count()
                                                    };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}