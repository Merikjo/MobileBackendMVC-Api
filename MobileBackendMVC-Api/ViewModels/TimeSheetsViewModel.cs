using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    public class TimeSheetsViewModel
    {
        public int Id_Timesheet { get; set; }
        public int? Id_Customer { get; set; }
        public int? Id_Contractor { get; set; }
        public int? Id_Employee { get; set; }
        public int? Id_WorkAssignment { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public string Comments { get; set; }
        public bool? WorkComplete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public string CustomerName { get; set; }

        public string CompanyName { get; set; }

        public string Title { get; set; }
        

        public virtual Contractors Contractors { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual WorkAssignments WorkAssignments { get; set; }
    }
}