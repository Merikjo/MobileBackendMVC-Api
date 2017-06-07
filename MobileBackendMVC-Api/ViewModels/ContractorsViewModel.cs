using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    public class ContractorsViewModel
    {
            public ContractorsViewModel()
            {
                this.Employees = new HashSet<Employees>();
                this.PinCodes = new HashSet<PinCodes>();
                this.Timesheets = new HashSet<Timesheets>();
            }

            public int Id_Contractor { get; set; }
            public int? Id_Department { get; set; }
            public string CompanyName { get; set; }
            public string ContactPerson { get; set; }
            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string VatId { get; set; }
            public double? HourlyRate { get; set; }

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

            public string DepartmentName { get; set; }

            public string Departments { get; set; }



            public virtual ICollection<Employees> Employees { get; set; }
           
            public virtual ICollection<PinCodes> PinCodes { get; set; }
            
            public virtual ICollection<Timesheets> Timesheets { get; set; }
        }
}