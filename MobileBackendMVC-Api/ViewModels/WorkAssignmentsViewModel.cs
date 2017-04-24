using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
   
    public class WorkAssignmentsViewModel
    {
        public WorkAssignmentsViewModel()
        {
            this.Timesheets = new HashSet<Timesheets>();
        }

        public int Id_WorkAssignment { get; set; }
        public Nullable<int> Id_Customer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public Nullable<bool> InProgress { get; set; }
        public Nullable<System.DateTime> InProgressAt { get; set; }
        public Nullable<bool> Completed { get; set; }
        public Nullable<System.DateTime> CompletedAt { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public bool? Active { get; set; }

        public string CustomerName { get; set; }

       
       

        public virtual Customers Customers { get; set; }
    
        public virtual ICollection<Timesheets> Timesheets { get; set; }
    }
}
