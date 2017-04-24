﻿using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackendMVC_Api.ViewModels
{
    public class TimeSheetsViewModel
    {
        public int Id_Timesheet { get; set; }
        public Nullable<int> Id_Customer { get; set; }
        public Nullable<int> Id_Contractor { get; set; }
        public Nullable<int> Id_Employee { get; set; }
        public Nullable<int> Id_WorkAssignment { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> StopTime { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> WorkComplete { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public bool? Active { get; set; }

        public virtual Contractors Contractors { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual WorkAssignments WorkAssignments { get; set; }
    }
}