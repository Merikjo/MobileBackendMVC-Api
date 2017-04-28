using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    
    public class CustomersViewModel
    {
        public CustomersViewModel()
        {
            this.PinCodes = new HashSet<PinCodes>();
            this.Timesheets = new HashSet<Timesheets>();
            this.WorkAssignments = new HashSet<WorkAssignments>();
        }

        public int Id_Customer { get; set; }
        public string CustomerName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public bool? Active { get; set; }

    
       

        public virtual ICollection<PinCodes> PinCodes { get; set; }

        public virtual ICollection<Timesheets> Timesheets { get; set; }
      
        public virtual ICollection<WorkAssignments> WorkAssignments { get; set; }
    }
}