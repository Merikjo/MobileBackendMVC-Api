using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedAt")]
        public DateTime? LastModifiedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DeletedAt")]
        public DateTime? DeletedAt { get; set; }

        public bool Active { get; set; }

        public int? Id_PinCode { get; set; }
        public string PinCode { get; set; }

       



        public virtual ICollection<PinCodes> PinCodes { get; set; }

        public virtual ICollection<Timesheets> Timesheets { get; set; }
      
        public virtual ICollection<WorkAssignments> WorkAssignments { get; set; }
    }
}