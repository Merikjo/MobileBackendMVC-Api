using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "StartTime")]
        public DateTime? StartTime { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "StopTime")]
        public DateTime? StopTime { get; set; }
        public string Comments { get; set; }
        public bool? WorkComplete { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedAt ")]
        public DateTime? LastModifiedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DeletedAt")]
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