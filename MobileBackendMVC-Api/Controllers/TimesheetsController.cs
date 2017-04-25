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

namespace MobileBackendMVC_Api.Controllers
{
    public class TimesheetsController : Controller
    {
        private JohaMeriSQL2Entities db = new JohaMeriSQL2Entities();

        // GET: Timesheets
        public ActionResult Index()
        {
            List<TimeSheetsViewModel> model = new List<TimeSheetsViewModel>();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            try
            {
                List<Timesheets> timesheets = entities.Timesheets.OrderByDescending(Timesheets => Timesheets.StartTime).ToList();

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

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        // GET: Timesheets/Details/5
        public ActionResult Details(int? id)
        {
            TimeSheetsViewModel model = new TimeSheetsViewModel();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
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

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
            TimeSheetsViewModel model = new TimeSheetsViewModel();

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName");
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName");
            //ViewBag.Id_WorkAssignment = new SelectList(db.WorkAssignments, "Id_WorkAssignment", "Title");

            return View(model);
        }//create

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeSheetsViewModel model)
        { 
            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            Timesheets tsv = new Timesheets();
            tsv.Id_Timesheet = model.Id_Timesheet;
            tsv.StartTime = model.StartTime;
            tsv.StopTime = model.StopTime;
            tsv.Comments = model.Comments;
            tsv.WorkComplete = model.WorkComplete;
            tsv.CreatedAt = DateTime.Now;
            tsv.LastModifiedAt = DateTime.Now;
            tsv.DeletedAt = model.DeletedAt;
            tsv.Active = model.Active;

            db.Timesheets.Add(tsv);

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", timesheets.Id_Contractor);
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName", timesheets.Id_Customer);
            //ViewBag.Id_WorkAssignment = new SelectList(db.WorkAssignments, "Id_WorkAssignment", "Title", timesheets.Id_WorkAssignment);

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
            view.StartTime = timesheetdetail.StartTime;
            view.StopTime = timesheetdetail.StopTime;
            view.Comments = timesheetdetail.Comments;
            view.WorkComplete = timesheetdetail.WorkComplete;
            view.CreatedAt = timesheetdetail.CreatedAt;
            view.LastModifiedAt = timesheetdetail.LastModifiedAt;
            view.DeletedAt = timesheetdetail.DeletedAt;
            view.Active = timesheetdetail.Active;

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", timesheets.Id_Contractor);
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName", timesheets.Id_Customer);
            //ViewBag.Id_WorkAssignment = new SelectList(db.WorkAssignments, "Id_WorkAssignment", "Title", timesheets.Id_WorkAssignment);

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
            tsv.CreatedAt = model.CreatedAt;
            tsv.LastModifiedAt = DateTime.Now;
            tsv.DeletedAt = model.DeletedAt;
            tsv.Active = model.Active;

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", timesheets.Id_Contractor);
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName", timesheets.Id_Customer);
            //ViewBag.Id_WorkAssignment = new SelectList(db.WorkAssignments, "Id_WorkAssignment", "Title", timesheets.Id_WorkAssignment);

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
