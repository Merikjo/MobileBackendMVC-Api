//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileBackendMVC_Api.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Departments
    {
        public int Id_Department { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<int> Id_Employee { get; set; }
    
        public virtual Employees Employees { get; set; }
    }
}
