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
using AutoMaintenance.ViewModels;
using System.Data.Entity.Infrastructure;

namespace AutoMaintenance.Controllers
{
    public class EmployeeIndexController : Controller
    {
        private AutoTrackContext db = new AutoTrackContext();

        // GET: EmployeeIndex
        public ActionResult Index(int? id, int? maintenanceID)
        {
            var viewModel = new EmployeeIndexData();
            viewModel.Employees = db.Employee
                .Include(e => e.Maintenances.Select(m => m.Vehicle))
                .OrderBy(e => e.LastName);

            if (id != null)
            {
                ViewBag.EmployeeID = id.Value;
                viewModel.Maintenances = viewModel.Employees.Where(
                    e => e.ID == id.Value).Single().Maintenances;
            }

            if (maintenanceID != null)
            {
                ViewBag.MaintenanceID = maintenanceID.Value;
                viewModel.Vehicle = viewModel.Maintenances.Where(
                    m => m.MaintenanceID == maintenanceID).Single().Vehicle;
            }

            return View(viewModel);
        }

        // GET: EmployeeIndex/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: EmployeeIndex/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeIndex/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: EmployeeIndex/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EmployeeIndex/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = db.Employee
               .Include(e => e.Maintenances)
               .Where(e => e.ID == id)
               .Single();

            if (TryUpdateModel(employeeToUpdate, "",
               new string[] { "LastName", "FirstMidName", "HireDate"}))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(employeeToUpdate);
        }

        // GET: EmployeeIndex/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EmployeeIndex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
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
    }
}
