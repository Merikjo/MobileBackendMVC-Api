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
    public class EmployeesController : Controller
    {
        private JohaMeriSQL2Entities db = new JohaMeriSQL2Entities();

        // GET: Employees
        public ActionResult Index()
        {
            List<EmployeesViewModel> model = new List<EmployeesViewModel>();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            try
            {
                List<Employees> employees = entities.Employees.OrderBy(Employees => Employees.LastName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Employees employee in employees)
                {
                    EmployeesViewModel view = new EmployeesViewModel();
                    view.Id_Employee = employee.Id_Employee;
                    view.FirstName = employee.FirstName;
                    view.LastName = employee.LastName;
                    view.PhoneNumber = employee.PhoneNumber;
                    view.EmailAddress = employee.EmailAddress;
                    view.EmployeeReference = employee.EmployeeReference;
                    view.DeletedAt = employee.DeletedAt;
                    view.Active = employee.Active;
                    view.EmployeePicture = employee.EmployeePicture;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }



            return View(model);
        }//Index

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            EmployeesViewModel model = new EmployeesViewModel();

            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
            try
            {
                Employees employeedetail = entities.Employees.Find(id);
                if (employeedetail == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    

                EmployeesViewModel view = new EmployeesViewModel();
                view.Id_Employee = employeedetail.Id_Employee;
                view.FirstName = employeedetail.FirstName;
                view.LastName = employeedetail.LastName;
                view.PhoneNumber = employeedetail.PhoneNumber;
                view.EmailAddress = employeedetail.EmailAddress;
                view.EmployeeReference = employeedetail.EmployeeReference;
                view.DeletedAt = employeedetail.DeletedAt;
                view.Active = employeedetail.Active;
                view.EmployeePicture = employeedetail.EmployeePicture;

                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details


        // GET: Employees/Create
        public ActionResult Create()
        {
            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
            EmployeesViewModel model = new EmployeesViewModel();
            return View(model);
        }//create

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeesViewModel model)
        {
            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();

            Employees evm = new Employees();
            evm.Id_Employee = model.Id_Employee;
            evm.FirstName = model.FirstName;
            evm.LastName = model.LastName;
            evm.PhoneNumber = model.PhoneNumber;
            evm.EmailAddress = model.EmailAddress;
            evm.EmployeeReference = model.EmployeeReference;
            evm.DeletedAt = model.DeletedAt;
            evm.Active = model.Active;
            evm.EmployeePicture = model.EmployeePicture;

            db.Employees.Add(evm);

            return RedirectToAction("Index");
        }//create


    // GET: Employees/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employeedetail = db.Employees.Find(id);
            if (employeedetail == null)
            {
                return HttpNotFound();
            }

            EmployeesViewModel view = new EmployeesViewModel();
            view.Id_Employee = employeedetail.Id_Employee;
            view.FirstName = employeedetail.FirstName;
            view.LastName = employeedetail.LastName;
            view.PhoneNumber = employeedetail.PhoneNumber;
            view.EmailAddress = employeedetail.EmailAddress;
            view.EmployeeReference = employeedetail.EmployeeReference;
            view.DeletedAt = employeedetail.DeletedAt;
            view.Active = employeedetail.Active;
            view.EmployeePicture = employeedetail.EmployeePicture;

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", employees.Id_Contractor);
            return View(view);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeesViewModel model)
        { 
            Employees evm = new Employees();
            evm.Id_Employee = model.Id_Employee;
            evm.FirstName = model.FirstName;
            evm.LastName = model.LastName;
            evm.PhoneNumber = model.PhoneNumber;
            evm.EmailAddress = model.EmailAddress;
            evm.EmployeeReference = model.EmployeeReference;
            evm.DeletedAt = model.DeletedAt;
            evm.Active = model.Active;
            evm.EmployeePicture = model.EmployeePicture;

            //ViewBag.Id_Contractor = new SelectList(db.Contractors, "Id_Contractor", "CompanyName", employees.Id_Contractor);
            db.SaveChanges();
            return View("Index");
        }//edit

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employeedetail = db.Employees.Find(id);
            if (employeedetail == null)
            {
                return HttpNotFound();
            }

            EmployeesViewModel view = new EmployeesViewModel();
            view.Id_Employee = employeedetail.Id_Employee;
            view.FirstName = employeedetail.FirstName;
            view.LastName = employeedetail.LastName;
            view.PhoneNumber = employeedetail.PhoneNumber;
            view.EmailAddress = employeedetail.EmailAddress;
            view.EmployeeReference = employeedetail.EmployeeReference;
            view.DeletedAt = employeedetail.DeletedAt;
            view.Active = employeedetail.Active;
            view.EmployeePicture = employeedetail.EmployeePicture;

            return View(view);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
