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
    public class ContractorsController : Controller
    {
        private MobileWorkDataEntities db = new MobileWorkDataEntities();

        // GET: Contractors
        public ActionResult Index()
        {

            List<ContractorsViewModel> model = new List<ContractorsViewModel>();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();

            try
            {
                List<Contractors> contractors = entities.Contractors.OrderBy(Contractors => Contractors.CompanyName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Contractors contractor in contractors)
                {
                    ContractorsViewModel view = new ContractorsViewModel();
                    view.Id_Contractor = contractor.Id_Contractor;
                    view.CompanyName = contractor.CompanyName;
                    view.ContactPerson = contractor.ContactPerson;
                    view.PhoneNumber = contractor.PhoneNumber;
                    view.EmailAddress = contractor.EmailAddress;
                    view.VatId = contractor.VatId;
                    view.HourlyRate = contractor.HourlyRate;
                    view.CreatedAt = contractor.CreatedAt;
                    view.LastModifiedAt = contractor.LastModifiedAt;
                    view.DeletedAt = contractor.DeletedAt;
                    view.Active = contractor.Active;

                    view.Id_PinCode = contractor.PinCodes.FirstOrDefault()?.Id_PinCode;
                    view.PinCode = contractor.PinCodes.FirstOrDefault()?.PinCode;
                    ViewBag.PinCode = new SelectList((from u in db.PinCodes select new { Id_PinCode = u.Id_PinCode, PinCode = u.PinCode }), "Id_PinCode", "PinCode", null);

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index


        // GET: Contractors/Details/5
        public ActionResult Details(int? id)
        {
            ContractorsViewModel model = new ContractorsViewModel();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                Contractors contractordetail = entities.Contractors.Find(id);
                if (contractordetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                ContractorsViewModel view = new ContractorsViewModel();
                view.Id_Contractor = contractordetail.Id_Contractor;
                view.CompanyName = contractordetail.CompanyName;
                view.ContactPerson = contractordetail.ContactPerson;
                view.PhoneNumber = contractordetail.PhoneNumber;
                view.EmailAddress = contractordetail.EmailAddress;
                view.VatId = contractordetail.VatId;
                view.HourlyRate = contractordetail.HourlyRate;
                view.CreatedAt = contractordetail.CreatedAt;
                view.LastModifiedAt = contractordetail.LastModifiedAt;
                view.DeletedAt = contractordetail.DeletedAt;
                view.Active = contractordetail.Active;

                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details


        // GET: Contractors/Create
        public ActionResult Create()
        {
            MobileWorkDataEntities db = new MobileWorkDataEntities();

            ContractorsViewModel model = new ContractorsViewModel();

            return View(model);
        }//create

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContractorsViewModel model)
        {
            MobileWorkDataEntities db = new MobileWorkDataEntities();

            Contractors con = new Contractors();
            con.CompanyName = model.CompanyName;
            con.ContactPerson = model.ContactPerson;
            con.PhoneNumber = model.PhoneNumber;
            con.EmailAddress = model.EmailAddress;
            con.VatId = model.VatId;
            con.HourlyRate = model.HourlyRate;
            con.CreatedAt = DateTime.Now;
            con.LastModifiedAt = DateTime.Now;
            con.DeletedAt = model.DeletedAt;
            con.Active = model.Active;

            db.Contractors.Add(con);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create


        // GET: Contractors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractors contractordetail = db.Contractors.Find(id);
            if (contractordetail == null)
            {
                return HttpNotFound();
            }
            ContractorsViewModel view = new ContractorsViewModel();
            view.Id_Contractor = contractordetail.Id_Contractor;
            view.CompanyName = contractordetail.CompanyName;
            view.ContactPerson = contractordetail.ContactPerson;
            view.PhoneNumber = contractordetail.PhoneNumber;
            view.EmailAddress = contractordetail.EmailAddress;
            view.VatId = contractordetail.VatId;
            view.HourlyRate = contractordetail.HourlyRate;
            view.CreatedAt = contractordetail.CreatedAt;
            view.LastModifiedAt = contractordetail.LastModifiedAt;
            view.DeletedAt = contractordetail.DeletedAt;
            view.Active = contractordetail.Active;
        
            return View(view);
    }

    // POST: Contractors/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContractorsViewModel model)
        {
            Contractors con = new Contractors();
            con.CompanyName = model.CompanyName;
            con.ContactPerson = model.ContactPerson;
            con.PhoneNumber = model.PhoneNumber;
            con.EmailAddress = model.EmailAddress;
            con.VatId = model.VatId;
            con.HourlyRate = model.HourlyRate;
            con.CreatedAt = model.CreatedAt;
            con.LastModifiedAt = DateTime.Now;
            con.DeletedAt = model.DeletedAt;
            con.Active = model.Active;

            db.SaveChanges();
            return View("Index");
        }//edit



        // GET: Contractors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractors contractordetail = db.Contractors.Find(id);
            if (contractordetail == null)
            {
                return HttpNotFound();
            }
            ContractorsViewModel view = new ContractorsViewModel();
            view.Id_Contractor = contractordetail.Id_Contractor;
            view.CompanyName = contractordetail.CompanyName;
            view.ContactPerson = contractordetail.ContactPerson;
            view.PhoneNumber = contractordetail.PhoneNumber;
            view.EmailAddress = contractordetail.EmailAddress;
            view.VatId = contractordetail.VatId;
            view.HourlyRate = contractordetail.HourlyRate;
            view.CreatedAt = contractordetail.CreatedAt;
            view.LastModifiedAt = DateTime.Now;
            view.DeletedAt = DateTime.Now;
            view.Active = contractordetail.Active;

            return View(view);
        }



        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contractors contractors = db.Contractors.Find(id);
            db.Contractors.Remove(contractors);
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
