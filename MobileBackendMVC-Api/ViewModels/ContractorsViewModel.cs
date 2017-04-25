using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
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
            public string CompanyName { get; set; }
            public string ContactPerson { get; set; }
            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string VatId { get; set; }
            public Nullable<double> HourlyRate { get; set; }
            public Nullable<System.DateTime> CreatedAt { get; set; }
            public Nullable<System.DateTime> LastModifiedAt { get; set; }
            public Nullable<System.DateTime> DeletedAt { get; set; }
            public bool? Active { get; set; }

        
            public virtual ICollection<Employees> Employees { get; set; }
           
            public virtual ICollection<PinCodes> PinCodes { get; set; }
            
            public virtual ICollection<Timesheets> Timesheets { get; set; }
        }
}