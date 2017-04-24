﻿using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobileBackendMVC_Api.Controllers
{
    public class EmployeeController : ApiController
    {
        public string[] GetAll()
        {
            string[] employeeNames = null;
            JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
            try
            {
                employeeNames = (from e in entities.Employees
                                 where (e.Active == true)
                                 select e.FirstName + " " +
                                 e.LastName).ToArray();
            }
            finally
            {
                entities.Dispose();
            }

            return employeeNames;
        }

        //public byte[] GetEmployeeImage(string employeeName)
        //{
        //    JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
        //    try
        //    {
        //        string[] nameParts = employeeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        //        string first = nameParts[0];
        //        string last = nameParts[1];
        //        byte[] bytes = (from e in entities.Employees
        //                        where (e.Active == true) &&
        //                        (e.FirstName == first) &&
        //                        (e.LastName == last)
        //                        select e.EmployeePicture).FirstOrDefault();

        //        return bytes;
        //    }
        //    finally
        //    {
        //        entities.Dispose();
        //    }
        //}

        //public string PutEmployeeImage()
        //{
        //    JohaMeriSQL2Entities entities = new JohaMeriSQL2Entities();
        //    try
        //    {
        //        Employees newEmployee = new Employees()
        //        {
        //            FirstName = "Heebo",
        //            LastName = "X",
        //            EmployeePicture = File.ReadAllBytes(@"C:\Temp\Heebo.png")
        //        };
        //        entities.Employees.Add(newEmployee);
        //        entities.SaveChanges();

        //        return "OK!";
        //    }
        //    finally
        //    {
        //        entities.Dispose();
        //    }

        //    return "Error";
        //}
    }
}
