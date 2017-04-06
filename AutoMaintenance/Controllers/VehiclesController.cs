using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMaintenance.Models;
using AutoMaintenance.DAL;
using System.Data.Entity.Infrastructure;

namespace AutoMaintenance.Controllers
{
    public class VehiclesController : Controller
    {
        private AutoTrackContext db = new AutoTrackContext();

        // GET: Vehicles
        public ActionResult Index(string vehicleModel, string searchString)
        {
            var ModelLst = new List<string>();
            var ModelQry = from d in db.Vehicle
                           orderby d.Model
                           select d.Model;
            ModelLst.AddRange(ModelQry.Distinct());
            ViewBag.vehicleModel = new SelectList(ModelLst);

            var vehicles = from v in db.Vehicle.Include(v=>v.VehicleType) select v;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => s.Make.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(vehicleModel))
            {
                vehicles = vehicles.Where(x => x.Model == vehicleModel);
            }

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            var vehicleTypesQuery = from vt in db.VehicleType select vt;
            ViewBag.VehicleTypeID = new SelectList(vehicleTypesQuery, "ID", "Type");

            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Make,Model,Year,Odometer,Rating,VehicleTypeID")] Vehicle vehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Vehicle.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Make", "Model", "Year", "Odometer", "Rating", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleToUpdate = db.Vehicle.Find(id);

            if (vehicleToUpdate == null)
            {
                Vehicle deletedVehicle = new Vehicle();
                TryUpdateModel(deletedVehicle, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The vehicle was deleted by another user.");
                return View(deletedVehicle);
            }

            if (TryUpdateModel(vehicleToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(vehicleToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Vehicle)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The vehicle was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Vehicle)databaseEntry.ToObject();

                        if (databaseValues.Make != clientValues.Make)
                            ModelState.AddModelError("Make", "Current value: " + databaseValues.Make);
                        if (databaseValues.Model != clientValues.Model)
                            ModelState.AddModelError("Model", "Current value: " + databaseValues.Model);
                        if (databaseValues.Year != clientValues.Year)
                            ModelState.AddModelError("Year", "Current value: " + databaseValues.Year);
                        if (databaseValues.Odometer != clientValues.Odometer)
                            ModelState.AddModelError("Odometer", "Current value: " + databaseValues.Odometer);
                        if (databaseValues.Rating != clientValues.Rating)
                            ModelState.AddModelError("Rating", "Current value: " + databaseValues.Rating);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        vehicleToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(vehicleToUpdate);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Vehicle vehicle)
        {
            try
            {
                db.Entry(vehicle).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = vehicle.ID });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(vehicle);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
