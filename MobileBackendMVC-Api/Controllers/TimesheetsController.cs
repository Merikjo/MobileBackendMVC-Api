using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileBackendMVC_Api.DataAccess;
using MobileBackendMVC_Api.ViewModels;
using System.Globalization;

namespace MobileBackendMVC_Api.Controllers
{
    public class TimesheetsController : Controller
    {
        private JohaMeriSQL5Entities db = new JohaMeriSQL5Entities();

        // GET: Timesheets
        public ActionResult Index()
        {
            List<TimeSheetsViewModel> model = new List<TimeSheetsViewModel>();

            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();

            try
            {
                //List<Timesheets> timesheets = entities.Timesheets.OrderByDescending(Timesheets => Timesheets.StartTime).ToList();
                List<Timesheets> timesheets = entities.Timesheets.OrderBy(Timesheets => Timesheets.StartTime).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Timesheets timesheet in timesheets)
                {
                    TimeSheetsViewModel view = new TimeSheetsViewModel();
                    view.Id_Timesheet = timesheet.Id_Timesheet;
                    view.StartTime = timesheet.StartTime;
                    view.StopTime = timesheet.StopTime;
                    view.Comments = timesheet.Comments;
                    view.WorkComplete = timesheet.WorkComplete;
                    view.CreatedAt = timesheet.CreatedAt;
                    view.LastModifiedAt = timesheet.LastModifiedAt;
                    view.DeletedAt = timesheet.DeletedAt;
                    view.Active = timesheet.Active;

                    view.Id_Employee = timesheet.Id_Employee;
                    view.FirstName = timesheet.Employees?.FirstName;
                    view.LastName = timesheet.Employees?.LastName;

                    view.Id_Customer = timesheet.Customers?.Id_Customer;
                    view.CustomerName = timesheet.Customers?.CustomerName;
                    ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

                    view.Id_Contractor = timesheet.Contractors?.Id_Contractor;
                    view.CompanyName = timesheet.Contractors?.CompanyName;
                    ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);

                    view.Id_WorkAssignment = timesheet.WorkAssignments?.Id_WorkAssignment;
                    view.Title = timesheet.WorkAssignments?.Title;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }
            CultureInfo fiFi = new CultureInfo("fi-FI");

            return View(model);
        }//Index

        // GET: Timesheets/Details/5
        public ActionResult Details(int? id)
        {
            TimeSheetsViewModel model = new TimeSheetsViewModel();

            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();
            try
            {
                Timesheets timesheetdetail = entities.Timesheets.Find(id);
                if (timesheetdetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                TimeSheetsViewModel view = new TimeSheetsViewModel();
                view.Id_Timesheet = timesheetdetail.Id_Timesheet;
                view.StartTime = timesheetdetail.StartTime;
                view.StopTime = timesheetdetail.StopTime;
                view.Comments = timesheetdetail.Comments;
                view.WorkComplete = timesheetdetail.WorkComplete;
                view.CreatedAt = timesheetdetail.CreatedAt;
                view.LastModifiedAt = timesheetdetail.LastModifiedAt;
                view.DeletedAt = timesheetdetail.DeletedAt;
                view.Active = timesheetdetail.Active;

                view.Id_Employee = timesheetdetail.Id_Employee;
                view.FirstName = timesheetdetail.Employees?.FirstName;
                view.LastName = timesheetdetail.Employees?.LastName;

                view.Id_Customer = timesheetdetail.Customers?.Id_Customer;
                view.CustomerName = timesheetdetail.Customers?.CustomerName;
                ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

                view.Id_Contractor = timesheetdetail.Contractors?.Id_Contractor;
                view.CompanyName = timesheetdetail.Contractors?.CompanyName;
                ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);

                view.Id_WorkAssignment = timesheetdetail.WorkAssignments?.Id_WorkAssignment;
                view.Title = timesheetdetail.WorkAssignments?.Title;


                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details


        // GET: Timesheets/Create
            public ActionResult Create()
        {
            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();

            TimeSheetsViewModel model = new TimeSheetsViewModel();

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);
            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);


            return View(model);
        }//create

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeSheetsViewModel model)
        { 
            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();

            Timesheets tsv = new Timesheets();
            tsv.Id_Timesheet = model.Id_Timesheet;
            tsv.StartTime = model.StartTime;
            tsv.StopTime = model.StopTime.GetValueOrDefault();
            tsv.Comments = model.Comments;
            tsv.CreatedAt = DateTime.Now;
            tsv.LastModifiedAt = DateTime.Now;
            tsv.Active = model.Active;

            db.Timesheets.Add(tsv);

            int employeeId = int.Parse(model.FirstName);
            if (employeeId > 0)
            {
                Employees emp = db.Employees.Find(employeeId);
                tsv.Id_Employee = emp.Id_Employee;
            }

            int employeeLastId = int.Parse(model.LastName);
            if (employeeLastId > 0)
            {
                Employees emp = db.Employees.Find(employeeLastId);
                tsv.Id_Employee = emp.Id_Employee;
            }

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);
            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create


        // GET: Timesheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheets timesheetdetail = db.Timesheets.Find(id);
            if (timesheetdetail == null)
            {
                return HttpNotFound();
            }

            TimeSheetsViewModel view = new TimeSheetsViewModel();
            view.Id_Timesheet = timesheetdetail.Id_Timesheet;
            view.StartTime = timesheetdetail.StartTime.Value;
            view.StopTime = timesheetdetail.StopTime.GetValueOrDefault();
            view.Comments = timesheetdetail.Comments;
            view.WorkComplete = timesheetdetail.WorkComplete;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = timesheetdetail.DeletedAt;
            view.Active = timesheetdetail.Active;

            view.Id_Employee = timesheetdetail.Id_Employee;
            view.FirstName = timesheetdetail.Employees?.FirstName;
            view.LastName = timesheetdetail.Employees?.LastName;

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", view.Id_Customer);
            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", view.Id_Contractor);

            return View(view);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TimeSheetsViewModel model)
        {
            Timesheets tsv = new Timesheets();
            tsv.Id_Timesheet = model.Id_Timesheet;
            tsv.StartTime = model.StartTime;
            tsv.StopTime = model.StopTime;
            tsv.Comments = model.Comments;
            tsv.WorkComplete = model.WorkComplete;
            tsv.LastModifiedAt = DateTime.Now;
            tsv.DeletedAt = model.DeletedAt;
            tsv.Active = model.Active;

            int employeeId = int.Parse(model.FirstName);
            if (employeeId > 0)
            {
                Employees emp = db.Employees.Find(employeeId);
                tsv.Id_Employee = emp.Id_Employee;
            }

            int employeeLastId = int.Parse(model.LastName);
            if (employeeLastId > 0)
            {
                Employees emp = db.Employees.Find(employeeLastId);
                tsv.Id_Employee = emp.Id_Employee;
            }

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", tsv.Id_Customer);
            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", tsv.Id_Contractor);

            db.SaveChanges();
            return View("Index");
        }

        // GET: Timesheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheets timesheetdetail = db.Timesheets.Find(id);
            if (timesheetdetail == null)
            {
                return HttpNotFound();
            }

            TimeSheetsViewModel view = new TimeSheetsViewModel();
            view.Id_Timesheet = timesheetdetail.Id_Timesheet;
            view.StartTime = timesheetdetail.StartTime;
            view.StopTime = timesheetdetail.StopTime;
            view.Comments = timesheetdetail.Comments;
            view.WorkComplete = timesheetdetail.WorkComplete;
            view.CreatedAt = timesheetdetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = DateTime.Now;
            view.Active = timesheetdetail.Active;

            view.Id_Employee = timesheetdetail.Id_Employee;
            view.FirstName = timesheetdetail.Employees?.FirstName;
            view.LastName = timesheetdetail.Employees?.LastName;

            return View(view);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheets timesheets = db.Timesheets.Find(id);
            db.Timesheets.Remove(timesheets);
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
