using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMaintenance.DAL;
using AutoMaintenance.Models;
using PagedList;
using AutoMaintenance.ViewModels;
using System.Data.Entity.Infrastructure;

namespace AutoMaintenance.Controllers
{
    public class MaintenancesController : Controller
    {
        private AutoTrackContext db = new AutoTrackContext();

        // GET: Maintenances
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //var maintenance = db.Maintenance.Include(m => m.Vehicle);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MakeSortParm = String.IsNullOrEmpty(sortOrder) ? "make_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //eager loading
            var maintenance = from m in db.Maintenance.Include(m => m.Vehicle) select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                maintenance = maintenance.Where(m => m.Vehicle.Make.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "make_desc":
                    maintenance = maintenance.OrderByDescending(m => m.Vehicle.Make);
                    break;
                case "Date":
                    maintenance = maintenance.OrderBy(m => m.Date);
                    break;
                case "date_desc":
                    maintenance = maintenance.OrderByDescending(m => m.Date);
                    break;
                default:
                    maintenance = maintenance.OrderBy(m => m.Vehicle.Make);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(maintenance.ToPagedList(pageNumber, pageSize));
        }

        // GET: Maintenances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance maintenance = db.Maintenance.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // GET: Maintenances/Create
        public ActionResult Create()
        {
            //ViewBag.VehicleID = new SelectList(db.Vehicle, "ID", "Make");
            PopulateVehiclesDropDownList();
            return View();
        }

        // POST: Maintenances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaintenanceID,VehicleID,Date,Price,Type")] Maintenance maintenance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Maintenance.Add(maintenance);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            //ViewBag.VehicleID = new SelectList(db.Vehicle, "ID", "Make", maintenance.VehicleID);
            PopulateVehiclesDropDownList(maintenance.VehicleID);
            return View(maintenance);
        }

        // GET: Maintenances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Maintenance maintenance = db.Maintenance.Include( m => m.Employees)
                .Where(m => m.MaintenanceID == id).Single();
            PopulateAssignedEmployeeData(maintenance);

            if (maintenance == null)
            {
                return HttpNotFound();
            }

            //ViewBag.VehicleID = new SelectList(db.Vehicle, "ID", "Make", maintenance.VehicleID);
            PopulateVehiclesDropDownList(maintenance.VehicleID);
            return View(maintenance);
        }

        private void PopulateAssignedEmployeeData(Maintenance maintenance)
        {
            var allEmployees = db.Employee;
            var maintenanceEmployee = new HashSet<int>(maintenance.Employees.Select(e => e.ID));
            var viewModel = new List<AssignedEmployeeData>();

            foreach(var employee in allEmployees)
            {
                viewModel.Add(new AssignedEmployeeData
                {
                    ID = employee.ID,
                    LastName = employee.LastName,
                    FirstName = employee.FirstMidName,
                    Assigned = maintenanceEmployee.Contains(employee.ID)
                });
            }

            ViewBag.Employees = viewModel;
        }

        // POST: Maintenances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var maintenanceToUpdate = db.Maintenance.Find(id);
            if (TryUpdateModel(maintenanceToUpdate, "",
               new string[] { "VehicleID", "Date", "Price", "Type" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            PopulateVehiclesDropDownList(maintenanceToUpdate.VehicleID);
            return View(maintenanceToUpdate);
        }

        // GET: Maintenances/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Maintenance maintenance = db.Maintenance.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // POST: Maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //Maintenance maintenance = db.Maintenance.Find(id);
                //db.Maintenance.Remove(maintenance);
                Maintenance maintenanceToDelete = new Maintenance() { MaintenanceID = id };
                db.Entry(maintenanceToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateVehiclesDropDownList(object selectedVehicle = null)
        {
            var vehiclesQuery = from v in db.Vehicle
                                orderby v.Make
                                select v;
            ViewBag.VehicleID = new SelectList(vehiclesQuery, "ID", "Make", selectedVehicle);
        }
    }
}
