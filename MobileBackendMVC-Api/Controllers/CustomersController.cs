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
    public class CustomersController : Controller
    {
        private JohaMeriSQL5Entities db = new JohaMeriSQL5Entities();

        // GET: Customers
        public ActionResult Index()
        {
            List<CustomersViewModel> model = new List<CustomersViewModel>();

            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();

            try
            {
                List<Customers> customers = entities.Customers.OrderBy(Customers => Customers.CustomerName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Customers customer in customers)
                {
                    CustomersViewModel view = new CustomersViewModel();
                    view.Id_Customer = customer.Id_Customer;
                    view.CustomerName = customer.CustomerName;
                    view.ContactPerson = customer.ContactPerson;
                    view.PhoneNumber = customer.PhoneNumber;
                    view.EmailAddress = customer.EmailAddress;
                    view.CreatedAt = customer.CreatedAt;
                    view.LastModifiedAt = customer.LastModifiedAt;
                    view.DeletedAt = customer.DeletedAt;
                    view.Active = customer.Active;

                    view.Id_PinCode = customer.PinCodes.FirstOrDefault()?.Id_PinCode;
                    view.PinCode = customer.PinCodes.FirstOrDefault()?.PinCode;
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

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
           CustomersViewModel model = new CustomersViewModel();

            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();
            try
            {
                Customers customerdetail = entities.Customers.Find(id);
                if (customerdetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                CustomersViewModel view = new CustomersViewModel();
                view.Id_Customer = customerdetail.Id_Customer;
                view.CustomerName = customerdetail.CustomerName;
                view.ContactPerson = customerdetail.ContactPerson;
                view.PhoneNumber = customerdetail.PhoneNumber;
                view.EmailAddress = customerdetail.EmailAddress;
                view.CreatedAt = customerdetail.CreatedAt;
                view.LastModifiedAt = customerdetail.LastModifiedAt;
                view.DeletedAt = customerdetail.DeletedAt;
                view.Active = customerdetail.Active;

                view.Id_PinCode = customerdetail.PinCodes.FirstOrDefault()?.Id_PinCode;
                view.PinCode = customerdetail.PinCodes.FirstOrDefault()?.PinCode;
                ViewBag.PinCode = new SelectList((from u in db.PinCodes select new { Id_PinCode = u.Id_PinCode, PinCode = u.PinCode }), "Id_PinCode", "PinCode", null);


                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details



        // GET: Customers/Create
        public ActionResult Create()
        {
            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();

            CustomersViewModel model = new CustomersViewModel();

            return View(model);
        }//create

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomersViewModel model)
        {
            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();

            Customers cus = new Customers();
            cus.CustomerName = model.CustomerName;
            cus.ContactPerson = model.ContactPerson;
            cus.PhoneNumber = model.PhoneNumber;
            cus.EmailAddress = model.EmailAddress;
            cus.CreatedAt = DateTime.Now;
            cus.LastModifiedAt = DateTime.Now;
            cus.Active = model.Active;

            db.Customers.Add(cus);

            PinCodes pic = new PinCodes();
            pic.PinCode = model.PinCode;
            //usr.Password = "joku@joku.fi";
            pic.Customers = cus;
            ViewBag.PinCode = new SelectList((from p in db.PinCodes select new { Id_PinCode = p.Id_PinCode, PinCode = p.PinCode }), "Id_PinCode", "PinCode", null);

          
            db.PinCodes.Add(pic);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customerdetail = db.Customers.Find(id);
            if (customerdetail == null)
            {
                return HttpNotFound();
            }

            CustomersViewModel view = new CustomersViewModel();
            view.Id_Customer = customerdetail.Id_Customer;
            view.CustomerName = customerdetail.CustomerName;
            view.ContactPerson = customerdetail.ContactPerson;
            view.PhoneNumber = customerdetail.PhoneNumber;
            view.EmailAddress = customerdetail.EmailAddress;
            view.CreatedAt = customerdetail.CreatedAt;
            view.LastModifiedAt = customerdetail.LastModifiedAt;
            view.DeletedAt = customerdetail.DeletedAt;
            view.Active = customerdetail.Active;

            view.Id_PinCode = customerdetail.PinCodes.FirstOrDefault()?.Id_PinCode;
            view.PinCode = customerdetail.PinCodes.FirstOrDefault()?.PinCode;
            ViewBag.PinCode = new SelectList((from u in db.PinCodes select new { Id_PinCode = u.Id_PinCode, PinCode = u.PinCode }), "Id_PinCode", "PinCode", null);


            return View(view);
        }

    // POST: Customers/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomersViewModel model)
        { 
            Customers cus = new Customers();
            cus.CustomerName = model.CustomerName;
            cus.ContactPerson = model.ContactPerson;
            cus.PhoneNumber = model.PhoneNumber;
            cus.EmailAddress = model.EmailAddress;
            cus.CreatedAt = model.CreatedAt;
            cus.LastModifiedAt = DateTime.Now; ;
            cus.DeletedAt = model.DeletedAt;
            cus.Active = model.Active;

            if (cus.PinCodes == null)
            {
                PinCodes pic = new PinCodes();
                pic.PinCode = model.PinCode;
                //usr.Password = "joku@joku.fi";
                pic.Customers = cus;

                db.PinCodes.Add(pic);
            }
            else
            {
                PinCodes pic = cus.PinCodes.FirstOrDefault();
                if (pic != null)
                {
                    pic.PinCode = model.PinCode;
                }
            }

            ViewBag.PinCode = new SelectList((from u in db.PinCodes select new { Id_PinCode = u.Id_PinCode, PinCode = u.PinCode }), "Id_PinCode", "PinCode", null);

            db.SaveChanges();
            return View("Index");
        }//edit



        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customerdetail = db.Customers.Find(id);
            if (customerdetail == null)
            {
                return HttpNotFound();
            }
            CustomersViewModel view = new CustomersViewModel();
            view.Id_Customer = customerdetail.Id_Customer;
            view.CustomerName = customerdetail.CustomerName;
            view.ContactPerson = customerdetail.ContactPerson;
            view.PhoneNumber = customerdetail.PhoneNumber;
            view.EmailAddress = customerdetail.EmailAddress;
            view.CreatedAt = customerdetail.CreatedAt;
            view.LastModifiedAt = customerdetail.LastModifiedAt;
            view.DeletedAt = customerdetail.DeletedAt;
            view.Active = customerdetail.Active;

            view.Id_PinCode = customerdetail.PinCodes.FirstOrDefault()?.Id_PinCode;
            view.PinCode = customerdetail.PinCodes.FirstOrDefault()?.PinCode;
            ViewBag.PinCode = new SelectList((from u in db.PinCodes select new { Id_PinCode = u.Id_PinCode, PinCode = u.PinCode }), "Id_PinCode", "PinCode", null);


            return View(view);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
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


        //SideMenu:
        public ActionResult SideMenu()
        {
            return PartialView("SideMenu");
        }
    }
}
