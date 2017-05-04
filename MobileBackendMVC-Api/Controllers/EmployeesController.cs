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

                    view.Id_PinCode = employee.PinCodes.FirstOrDefault()?.Id_PinCode;
                    view.PinCode = employee.PinCodes.FirstOrDefault()?.PinCode;
                    ViewBag.PinCode = new SelectList((from u in db.PinCodes select new { Id_PinCode = u.Id_PinCode, PinCode = u.PinCode }), "Id_PinCode", "PinCode", null);


                    view.Id_Contractor = employee.Contractors?.Id_Contractor;
                    view.CompanyName = employee.Contractors?.CompanyName;

                    view.Id_Department = employee.Departments?.Id_Department;
                    view.DepartmentName = employee.Departments?.DepartmentName;

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

                view.Id_Contractor = employeedetail.Contractors?.Id_Contractor;
                view.CompanyName = employeedetail.Contractors?.CompanyName;

                view.Id_Department = employeedetail.Departments?.Id_Department;
                view.DepartmentName = employeedetail.Departments?.DepartmentName;

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

            ViewBag.DepartmentName = new SelectList((from d in db.Departments select new { Id_Department = d.Id_Department, DepartmentName = d.DepartmentName }), "Id_Department", "DepartmentName", null);

            ViewBag.CompanyName = new SelectList((from c in db.Contractors select new { Id_Contractor = c.Id_Contractor, CompanyName = c.CompanyName }), "Id_Contractor", "CompanyName", null);

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

            Employees emp = new Employees();
            emp.Id_Employee = model.Id_Employee;
            emp.FirstName = model.FirstName;
            emp.LastName = model.LastName;
            emp.PhoneNumber = model.PhoneNumber;
            emp.EmailAddress = model.EmailAddress;
            emp.EmployeeReference = model.EmployeeReference;
            emp.DeletedAt = model.DeletedAt;
            emp.Active = model.Active;
            emp.EmployeePicture = model.EmployeePicture;

            db.Employees.Add(emp);

            int contractorId = int.Parse(model.CompanyName);
            if (contractorId > 0)
            {
                Contractors con = db.Contractors.Find(contractorId);
                emp.Id_Contractor = con.Id_Contractor;
            }

            int departmentId = int.Parse(model.DepartmentName);
            if (departmentId > 0)
            {
                Departments dep = db.Departments.Find(departmentId);
                emp.Id_Department = dep.Id_Department;
            }

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            ViewBag.DepartmentName = new SelectList((from d in db.Departments select new { Id_Department = d.Id_Department, DepartmentName = d.DepartmentName }), "Id_Department", "DepartmentName", null);

            ViewBag.CompanyName = new SelectList((from c in db.Contractors select new { Id_Contractor = c.Id_Contractor, CustomerName = c.CompanyName }), "Id_Contractor", "CompanyName", null);

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

            view.Id_Contractor = employeedetail.Contractors?.Id_Contractor;
            view.CompanyName = employeedetail.Contractors?.CompanyName;

            view.Id_Department = employeedetail.Departments?.Id_Department;
            view.DepartmentName = employeedetail.Departments?.DepartmentName;

            ViewBag.DepartmentName = new SelectList((from d in db.Departments select new { Id_Department = d.Id_Department, DepartmentName = d.DepartmentName }), "Id_Department", "DepartmentName", null);

            ViewBag.CompanyName = new SelectList((from c in db.Contractors select new { Id_Contractor = c.Id_Contractor, CompanyName = c.CompanyName }), "Id_Contractor", "CompanyName", null);

            return View(view);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeesViewModel model)
        { 
            Employees emp = new Employees();
            emp.FirstName = model.FirstName;
            emp.LastName = model.LastName;
            emp.PhoneNumber = model.PhoneNumber;
            emp.EmailAddress = model.EmailAddress;
            emp.EmployeeReference = model.EmployeeReference;
            emp.DeletedAt = model.DeletedAt;
            emp.Active = model.Active;
            emp.EmployeePicture = model.EmployeePicture;

            int contractorId = int.Parse(model.CompanyName);
            if (contractorId > 0)
            {
                Contractors con = db.Contractors.Find(contractorId);
                emp.Id_Contractor = con.Id_Contractor;
            }

            int departmentId = int.Parse(model.DepartmentName);
            if (departmentId > 0)
            {
                Departments dep = db.Departments.Find(departmentId);
                emp.Id_Department = dep.Id_Department;
            }

            ViewBag.DepartmentName = new SelectList((from d in db.Departments select new { Id_Department = d.Id_Department, DepartmentName = d.DepartmentName }), "Id_Department", "DepartmentName", null);

            ViewBag.CompanyName = new SelectList((from c in db.Contractors select new { Id_Contractor = c.Id_Contractor, CompanyName = c.CompanyName }), "Id_Contractor", "CompanyName", null);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

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

            view.Id_Contractor = employeedetail.Contractors?.Id_Contractor;
            view.CompanyName = employeedetail.Contractors?.CompanyName;

            view.Id_Department = employeedetail.Departments?.Id_Department;
            view.DepartmentName = employeedetail.Departments?.DepartmentName;

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

        public ActionResult Geo()
        {
            return View();
        }
    }
}
