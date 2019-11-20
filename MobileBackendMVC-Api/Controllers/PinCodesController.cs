using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileBackendMVC_Api.DataAccess;
using MobileBackendMVC_Api.Models;
using MobileBackendMVC_Api.ViewModels;

namespace MobileBackendMVC_Api.Controllers
{
    public class PinCodesController : Controller
    {
        private MobileWorkDataEntities db = new MobileWorkDataEntities();

        // GET: PinCodes
        public ActionResult Index()
        {
            List<PinCodesViewModel> model = new List<PinCodesViewModel>();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();

            try
            {
                List<PinCodes> pincodes = entities.PinCodes.OrderBy(PinCodes => PinCodes.PinCode).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (PinCodes pincode in pincodes)
                {
                    PinCodesViewModel view = new PinCodesViewModel();
                    view.Id_PinCode = pincode.Id_PinCode;
                    view.PinCode = pincode.PinCode;             
                    view.CreatedAt = pincode.CreatedAt;
                    view.LastModifiedAt = pincode.LastModifiedAt;
                    view.DeletedAt = pincode.DeletedAt;
                    view.Active = pincode.Active;

                    view.Id_Customer = pincode.Customers?.Id_Customer;
                    view.CustomerName = pincode.Customers?.CustomerName;
                    ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

                    view.Id_Contractor = pincode.Contractors?.Id_Contractor;
                    view.CompanyName = pincode.Contractors?.CompanyName;
                    ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);

                    view.Id_Employee = pincode.Employees?.Id_Employee;
                    view.FirstName = pincode.Employees?.FirstName;
                    view.LastName = pincode.Employees?.LastName;


                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        // GET: PinCodes/Details/5
        public ActionResult Details(int? id)
        {
            PinCodesViewModel model = new PinCodesViewModel();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                PinCodes pincodedetail = entities.PinCodes.Find(id);
                if (pincodedetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                PinCodesViewModel view = new PinCodesViewModel();
                view.Id_PinCode = pincodedetail.Id_PinCode;
                view.PinCode = pincodedetail.PinCode;
                view.CreatedAt = pincodedetail.CreatedAt;
                view.LastModifiedAt = pincodedetail.LastModifiedAt;
                view.DeletedAt = pincodedetail.DeletedAt;
                view.Active = pincodedetail.Active;

                view.Id_Customer = pincodedetail.Customers?.Id_Customer;
                view.CustomerName = pincodedetail.Customers?.CustomerName;
                ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

                view.Id_Contractor = pincodedetail.Contractors?.Id_Contractor;
                view.CompanyName = pincodedetail.Contractors?.CompanyName;
                ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);

                view.Id_Employee = pincodedetail.Employees?.Id_Employee;
                view.FirstName = pincodedetail.Employees?.FirstName;
                view.LastName = pincodedetail.Employees?.LastName;

                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details

        // GET: PinCodes/Create
        public ActionResult Create()
        {
            MobileWorkDataEntities entities = new MobileWorkDataEntities();

            PinCodesViewModel model = new PinCodesViewModel();

         
            ViewBag.CompanyName = new SelectList((from c in db.Contractors select new { Id_Contractor = c.Id_Contractor, CompanyName = c.CompanyName }), "Id_Contractor", "CompanyName", null);
            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);
            ViewBag.FirstName = new SelectList((from c in db.Employees select new { Id_Employee = c.Id_Employee, FirstName = c.FirstName }), "Id_Employee", "FirstName", null);
            ViewBag.LastName = new SelectList((from c in db.Employees select new { Id_Employee = c.Id_Employee, LastName = c.LastName }), "Id_Employee", "LastName", null);


            return View(model);
        }

        // POST: PinCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PinCodesViewModel model)
        {
            MobileWorkDataEntities entities = new MobileWorkDataEntities();

            PinCodes pin = new PinCodes();
            pin.Id_PinCode = model.Id_PinCode;
            pin.PinCode = model.PinCode;
            pin.CreatedAt = DateTime.Now;
            pin.LastModifiedAt = DateTime.Now;
            pin.DeletedAt = model.DeletedAt;
            pin.Active = model.Active;

            db.PinCodes.Add(pin);

            int customerId = int.Parse(model.CustomerName);
            if (customerId > 0)
            {
                Customers cus = db.Customers.Find(customerId);
                pin.Id_Customer = cus.Id_Customer;
            }

            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", null);

            int contractorId = int.Parse(model.CompanyName);
            if (contractorId > 0)
            {
                Contractors con = db.Contractors.Find(contractorId);
                pin.Id_Contractor = con.Id_Contractor;
            }

            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", null);

            int employeeId = int.Parse(model.FirstName);
            if (employeeId > 0)
            {
                Employees emp = db.Employees.Find(employeeId);
                pin.Id_Employee = emp.Id_Employee;
            }

            int employLastId = int.Parse(model.LastName);
            if (employLastId > 0)
            {
                Employees emp = db.Employees.Find(employLastId);
                pin.Id_Employee = emp.Id_Employee;
            }


            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create


        // GET: PinCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PinCodes pincodedetail = db.PinCodes.Find(id);
            if (pincodedetail == null)
            {
                return HttpNotFound();
            }
   
            PinCodesViewModel view = new PinCodesViewModel();
            view.Id_PinCode = pincodedetail.Id_PinCode;
            view.PinCode = pincodedetail.PinCode;
            view.CreatedAt = pincodedetail.CreatedAt;
            view.LastModifiedAt = pincodedetail.LastModifiedAt;
            view.DeletedAt = pincodedetail.DeletedAt;
            view.Active = pincodedetail.Active;

            view.Id_Customer = pincodedetail.Customers?.Id_Customer;
            view.CustomerName = pincodedetail.Customers?.CustomerName;
            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", view.Id_Customer);

            view.Id_Contractor = pincodedetail.Contractors?.Id_Contractor;
            view.CompanyName = pincodedetail.Contractors?.CompanyName;
            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", view.Id_Contractor);

            view.Id_Employee = pincodedetail.Employees?.Id_Employee;
            view.FirstName = pincodedetail.Employees?.FirstName;
            view.LastName = pincodedetail.Employees?.LastName;

            return View(view);
        }//edit

        // POST: PinCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PinCodesViewModel model)
        {
            PinCodes pin = new PinCodes();
            pin.Id_PinCode = model.Id_PinCode;
            pin.PinCode = model.PinCode;
            pin.CreatedAt = model.CreatedAt;
            pin.LastModifiedAt = DateTime.Now;
            pin.DeletedAt = model.DeletedAt;
            pin.Active = model.Active;


            int customerId = model.Id_Customer ?? 0;
            if (customerId > 0)

            {
                Customers cus = db.Customers.Find(customerId);
                pin.Id_Customer = customerId;
                pin.Id_Customer = cus.Id_Customer;
            }
            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", pin.Id_Customer);

            int contractorId = model.Id_Contractor ?? 0;
            if (contractorId > 0)

            {
                Contractors con = db.Contractors.Find(contractorId);
                pin.Id_Contractor = customerId;
                pin.Id_Contractor = con.Id_Contractor;
            }

            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", pin.Id_Contractor);

            int employeeId = model.Id_Employee ?? 0;
            if (employeeId > 0)

            {
                Employees emp = db.Employees.Find(employeeId);
                pin.Id_Employee = employeeId;
                pin.Id_Employee = emp.Id_Employee;
                
            }
            ViewBag.FirstName = new SelectList((from c in db.Employees select new { Id_Employee = c.Id_Employee, FirstName = c.FirstName }), "Id_Employee", "FirstName", pin.Id_Employee);
            ViewBag.LastName = new SelectList((from c in db.Employees select new { Id_Employee = c.Id_Employee, LastName = c.LastName }), "Id_Employee", "LastName", pin.Id_Employee);

            db.SaveChanges();
            return RedirectToAction("Index");
        }//edit

        // GET: PinCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PinCodes pincodedetail = db.PinCodes.Find(id);
            if (pincodedetail == null)
            {
                return HttpNotFound();
            }

            PinCodesViewModel view = new PinCodesViewModel();
            view.Id_PinCode = pincodedetail.Id_PinCode;
            view.PinCode = pincodedetail.PinCode;
            view.CreatedAt = pincodedetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = DateTime.Now;
            view.Active = pincodedetail.Active;

            view.Id_Customer = pincodedetail.Customers?.Id_Customer;
            view.CustomerName = pincodedetail.Customers?.CustomerName;
            ViewBag.CustomerName = new SelectList((from c in db.Customers select new { Id_Customer = c.Id_Customer, CustomerName = c.CustomerName }), "Id_Customer", "CustomerName", view.Id_Customer);

            view.Id_Contractor = pincodedetail.Contractors?.Id_Contractor;
            view.CompanyName = pincodedetail.Contractors?.CompanyName;
            ViewBag.CompanyName = new SelectList((from co in db.Contractors select new { Id_Contractor = co.Id_Contractor, CompanyName = co.CompanyName }), "Id_Contractor", "CompanyName", view.Id_Contractor);

            view.Id_Employee = pincodedetail.Employees?.Id_Employee;
            view.FirstName = pincodedetail.Employees?.FirstName;
            view.LastName = pincodedetail.Employees?.LastName;

            return View(view);
        }



        // POST: PinCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PinCodes pinCodes = db.PinCodes.Find(id);
            db.PinCodes.Remove(pinCodes);
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
