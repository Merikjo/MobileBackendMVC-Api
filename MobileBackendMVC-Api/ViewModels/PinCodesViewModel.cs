using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    public class PinCodesViewModel
    {
        public int Id_PinCode { get; set; }
        public string PinCode { get; set; }
        public Nullable<int> Id_Employee { get; set; }
        public Nullable<int> Id_Customer { get; set; }
        public Nullable<int> Id_Contractor { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public Nullable<bool> Active { get; set; }

        public virtual Contractors Contractors { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Employees Employees { get; set; }
    }
}