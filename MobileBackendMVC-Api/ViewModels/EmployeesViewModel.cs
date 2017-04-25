using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    using MobileBackendMVC_Api.DataAccess;
    using System;
    using System.Collections.Generic;
    public class EmployeesViewModel
    {
        
        public EmployeesViewModel()
        {
            this.Departments = new HashSet<Departments>();
            this.PinCodes = new HashSet<PinCodes>();
        }

        public int Id_Employee { get; set; }
        public int? Id_Contractor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeReference { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public bool? Active { get; set; }

        public byte[] EmployeePicture { get; set; }

        public string CompanyName { get; set; }

        public string DepartmentName { get; set; }

        public int? Id_Department { get; set; }

        public virtual Contractors Contractors { get; set; }
     
        public virtual ICollection<Departments> Departments { get; set; }
       
        public virtual ICollection<PinCodes> PinCodes { get; set; }
    }
}