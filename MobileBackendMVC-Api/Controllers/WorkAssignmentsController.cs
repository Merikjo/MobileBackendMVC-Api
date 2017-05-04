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
    public class WorkAssignmentsController : Controller
    {
        private JohaMeriSQL2Entities db = new JohaMeriSQL2Entities();

        // GET: WorkAssignments
        public ActionResult Index()
        {
            List<WorkAssignmentsViewModel> model = new List<WorkAssignmentsViewModel>();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            try
            {
                List<WorkAssignments> workAssignments = entities.WorkAssignments.OrderBy(WorkAssignments => WorkAssignments.Deadline).ToList();

                CultureInfo fiFi = new CultureInfo("fi-FI");
               
                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (WorkAssignments workAssignment in workAssignments)
                {
                    WorkAssignmentsViewModel view = new WorkAssignmentsViewModel();
                    view.Id_WorkAssignment = workAssignment.Id_WorkAssignment;
                    view.Title = workAssignment.Title;
                    view.Description = workAssignment.Description;
                    view.Deadline = workAssignment.Deadline;
                    view.CreatedAt = workAssignment.CreatedAt;
                    view.InProgress = workAssignment.InProgress;
                    view.InProgressAt = workAssignment.InProgressAt;
                    view.CompletedAt = workAssignment.CompletedAt;
                    view.Completed = workAssignment.Completed;
                    view.LastModifiedAt = workAssignment.LastModifiedAt;
                    view.DeletedAt = workAssignment.DeletedAt;
                    view.Active = workAssignment.Active;

                    view.Id_Customer = workAssignment.Customers?.Id_Customer;
                    view.CustomerName = workAssignment.Customers?.CustomerName;

                    ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            

            return View(model);
        }//Index

        // GET: WorkAssignments/Details/5
        public ActionResult Details(int? id)
        {
            WorkAssignmentsViewModel model = new WorkAssignmentsViewModel();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
            try
            {

                WorkAssignments workassdetail = entities.WorkAssignments.Find(id);
                if (workassdetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta        
                WorkAssignmentsViewModel view = new WorkAssignmentsViewModel();
                view.Id_WorkAssignment = workassdetail.Id_WorkAssignment;
                view.Title = workassdetail.Title;
                view.Description = workassdetail.Description;
                view.Deadline = workassdetail.Deadline;
                view.CreatedAt = workassdetail.CreatedAt;
                view.InProgress = workassdetail.InProgress;
                view.InProgressAt = workassdetail.InProgressAt;
                view.CompletedAt = workassdetail.CompletedAt;
                view.Completed = workassdetail.Completed;
                view.LastModifiedAt = workassdetail.LastModifiedAt;
                view.DeletedAt = workassdetail.DeletedAt;
                view.Active = workassdetail.Active;

                view.Id_Customer = workassdetail.Customers?.Id_Customer;
                view.CustomerName = workassdetail.Customers?.CustomerName;
                ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", view.Id_Customer);

                model = view;

            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//details

        // GET: WorkAssignments/Create
        public ActionResult Create()
        {
            JohaMeriSQL2Entities db = new JohaMeriSQL2Entities();

            WorkAssignmentsViewModel model = new WorkAssignmentsViewModel();

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

            return View(model);
        }//create

        // POST: WorkAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkAssignmentsViewModel model)
        {
            JohaMeriSQL2Entities db = new JohaMeriSQL2Entities();

            WorkAssignments wam = new WorkAssignments();
            wam.Title = model.Title;
            wam.Description = model.Description;
            wam.Deadline = model.Deadline;
            wam.CreatedAt = DateTime.Now;
            wam.InProgress = model.InProgress;
            wam.InProgressAt = model.InProgressAt;
            wam.CompletedAt = model.CompletedAt;
            wam.Completed = model.Completed;
            wam.LastModifiedAt = DateTime.Now;
            wam.Active = true; ;

            db.WorkAssignments.Add(wam);

            int customerId = int.Parse(model.CustomerName);
            if (customerId > 0)
            {
                Customers cus = db.Customers.Find(customerId);
                wam.Id_Customer = cus.Id_Customer;       
            }

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }
           
            return RedirectToAction("Index");
        }//create

        CultureInfo fiFi = new CultureInfo("fi-FI");

        // GET: WorkAssignments/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignments workassdetail = db.WorkAssignments.Find(id);
            if (workassdetail == null)
            {
                return HttpNotFound();
            }

            WorkAssignmentsViewModel view = new WorkAssignmentsViewModel();
            view.Id_WorkAssignment = workassdetail.Id_WorkAssignment;
            view.Title = workassdetail.Title;
            view.Description = workassdetail.Description;
            view.Deadline = workassdetail.Deadline.GetValueOrDefault();
            view.CreatedAt = workassdetail.CreatedAt.GetValueOrDefault();
            view.InProgress = workassdetail.InProgress;
            view.InProgressAt = workassdetail.InProgressAt.GetValueOrDefault();
            view.CompletedAt = workassdetail.CompletedAt.GetValueOrDefault();
            view.Completed = workassdetail.Completed;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = workassdetail.DeletedAt.GetValueOrDefault();
            view.Active = workassdetail.Active;

            view.Id_Customer = workassdetail.Customers?.Id_Customer;
            view.CustomerName = workassdetail.Customers?.CustomerName;

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", view.Id_Customer);

            return View(view);
        }

        // POST: WorkAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (WorkAssignmentsViewModel model)
        {
            WorkAssignments wam = db.WorkAssignments.Find(model.Id_WorkAssignment);
            wam.Title = model.Title;
            wam.Description = model.Description;
            wam.Deadline = model.Deadline.GetValueOrDefault();
            wam.CreatedAt = model.CreatedAt.GetValueOrDefault();
            wam.InProgress = model.InProgress;
            wam.InProgressAt = model.InProgressAt.GetValueOrDefault();
            wam.CompletedAt = model.CompletedAt.GetValueOrDefault();
            wam.Completed = model.Completed;
            wam.LastModifiedAt = DateTime.Now;
            wam.DeletedAt = model.DeletedAt.GetValueOrDefault();
            wam.Active = true;

            int customerId = int.Parse(model.CustomerName);
            if (customerId > 0)
            {
                Customers cus = db.Customers.Find(customerId);
                wam.Id_Customer = cus.Id_Customer;
            }

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", wam.Id_Customer);

            db.SaveChanges();
            return RedirectToAction("Index");
        }//edit

        // GET: WorkAssignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignments workassdetail = db.WorkAssignments.Find(id);
            if (workassdetail == null)
            {
                return HttpNotFound();
            }

            WorkAssignmentsViewModel view = new WorkAssignmentsViewModel();
            view.Id_WorkAssignment = workassdetail.Id_WorkAssignment;
            view.Title = workassdetail.Title;
            view.Description = workassdetail.Description;
            view.Deadline = workassdetail.Deadline;
            view.InProgress = workassdetail.InProgress;
            view.InProgressAt = workassdetail.InProgressAt;
            view.CompletedAt = workassdetail.CompletedAt;
            view.Completed = workassdetail.Completed;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = DateTime.Now;
            view.Active = workassdetail.Active;

            view.Id_Customer = workassdetail.Customers?.Id_Customer;
            view.CustomerName = workassdetail.Customers?.CustomerName;

            return View(view);
        }

        // POST: WorkAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkAssignments workAssignments = db.WorkAssignments.Find(id);
            db.WorkAssignments.Remove(workAssignments);
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
        }//Delete
    }
}
