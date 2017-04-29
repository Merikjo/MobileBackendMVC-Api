using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    public class DepartmentsViewModel
    {
        public int Id_Department { get; set; }
        public string DepartmentName { get; set; }
        public int? Id_Employee { get; set; }
        public int? Id_PinCode { get; set; }

        public string PinCode { get; set; }
        public string PinCodes { get; set; }



        public virtual Employees Employees { get; set; }
    }
}