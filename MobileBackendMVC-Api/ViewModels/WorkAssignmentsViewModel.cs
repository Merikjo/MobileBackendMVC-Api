using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Work Deadline")]
        public DateTime? Deadline { get; set; }
        public bool? InProgress { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "In Progress")]
        public DateTime? InProgressAt { get; set; }
        public bool? Completed { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Work Completed")]
        public DateTime? CompletedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Work Started")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        public DateTime? LastModifiedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Work Archived")]
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public string CustomerName { get; set; }

       
       

        public virtual Customers Customers { get; set; }
    
        public virtual ICollection<Timesheets> Timesheets { get; set; }
    }
}
