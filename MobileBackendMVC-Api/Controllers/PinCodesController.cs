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
    public class PinCodesController : Controller
    {
        private JohaMeriSQL2Entities db = new JohaMeriSQL2Entities();

        // GET: PinCodes
        public ActionResult Index()
        {
            List<PinCodesViewModel> model = new List<PinCodesViewModel>();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            try
            {
                List<PinCodes> pincodes = entities.PinCodes.OrderBy(PinCodes => PinCodes.PinCode).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (PinCodes pincode in pincodes)
                {
                    PinCodesViewModel view = new PinCodesViewModel();
                    view.Id_Contractor = pincode.Id_Contractor;
                    view.PinCode = pincode.PinCode;             
                    view.CreatedAt = pincode.CreatedAt;
                    view.LastModifiedAt = pincode.LastModifiedAt;
                    view.DeletedAt = pincode.DeletedAt;
                    view.Active = pincode.Active;

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

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
            try
            {
                PinCodes pincodedetail = entities.PinCodes.Find(id);
                if (pincodedetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                PinCodesViewModel view = new PinCodesViewModel();
                view.Id_Contractor = pincodedetail.Id_Contractor;
                view.PinCode = pincodedetail.PinCode;
                view.CreatedAt = pincodedetail.CreatedAt;
                view.LastModifiedAt = pincodedetail.LastModifiedAt;
                view.DeletedAt = pincodedetail.DeletedAt;
                view.Active = pincodedetail.Active;

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
            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            PinCodesViewModel model = new PinCodesViewModel();

            ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName");
            ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName");
            ViewBag.Id_Employee = new SelectList(db.Employees, "Id_Employee", "FirstName");

            return View(model);
        }

        // POST: PinCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PinCodesViewModel model)
        {
            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            PinCodes pin = new PinCodes();
            pin.Id_Contractor = model.Id_Contractor;
            pin.PinCode = model.PinCode;
            pin.CreatedAt = DateTime.Now;
            pin.LastModifiedAt = DateTime.Now;
            pin.DeletedAt = model.DeletedAt;
            pin.Active = model.Active;

            db.PinCodes.Add(pin);

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", pinCodes.Id_Contractor);
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName", pinCodes.Id_Customer);
            //ViewBag.Id_Employee = new SelectList(db.Employees, "Id_Employee", "FirstName", pinCodes.Id_Employee);

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
            view.Id_Contractor = pincodedetail.Id_Contractor;
            view.PinCode = pincodedetail.PinCode;
            view.CreatedAt = pincodedetail.CreatedAt;
            view.LastModifiedAt = pincodedetail.LastModifiedAt;
            view.DeletedAt = pincodedetail.DeletedAt;
            view.Active = pincodedetail.Active;

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", pinCodes.Id_Contractor);
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName", pinCodes.Id_Customer);
            //ViewBag.Id_Employee = new SelectList(db.Employees, "Id_Employee", "FirstName", pinCodes.Id_Employee);

            return View(view);
        }

        // POST: PinCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PinCodesViewModel model)
        {
            PinCodes pin = new PinCodes();
            pin.Id_Contractor = model.Id_Contractor;
            pin.PinCode = model.PinCode;
            pin.CreatedAt = model.CreatedAt;
            pin.LastModifiedAt = DateTime.Now;
            pin.DeletedAt = model.DeletedAt;
            pin.Active = model.Active;

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", pinCodes.Id_Contractor);
            //ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "CustomerName", pinCodes.Id_Customer);
            //ViewBag.Id_Employee = new SelectList(db.Employees, "Id_Employee", "FirstName", pinCodes.Id_Employee);

            db.SaveChanges();
            return View("Index");
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
            view.Id_Contractor = pincodedetail.Id_Contractor;
            view.PinCode = pincodedetail.PinCode;
            view.CreatedAt = pincodedetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = DateTime.Now;
            view.Active = pincodedetail.Active;

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
