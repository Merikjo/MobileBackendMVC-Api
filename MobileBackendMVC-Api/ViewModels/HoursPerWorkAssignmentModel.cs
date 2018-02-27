
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    public class HoursPerWorkAssignmentModel
    {
        public int Id_WorkAssignment { get; set; }
        public string WorkAssignmentName { get; set; }
        public double TotalHours { get; set; }

        public bool WorkComplete { get; set; }
    }
}