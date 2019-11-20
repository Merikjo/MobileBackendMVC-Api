using MobileBackendMVC_Api.DataAccess;
using MobileBackendMVC_Api.Models;
using MobileBackendMVC_Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace MobileBackendMVC_Api.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult HoursPerWorkAssignment()
        {

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Timesheets> allTimesheetsToday = (from ts in entities.Timesheets
                                                       where (ts.StartTime > today) &&
                                                       (ts.StartTime < tomorrow) 
                                                       //&& (ts.WorkComplete == true)
                                                       select ts).ToList();

                // ryhmitellään kirjaukset tehtävittäin, ja lasketaan kestot
                List<HoursPerWorkAssignmentModel> model = new List<HoursPerWorkAssignmentModel>();

                foreach (Timesheets timesheet in allTimesheetsToday)
                {
                    int assignmentId = timesheet.Id_WorkAssignment.Value;
                    HoursPerWorkAssignmentModel existing = model.Where(
                        m => m.Id_WorkAssignment == assignmentId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.TotalHours += (timesheet.StopTime.Value - timesheet.StartTime.Value).TotalHours;
                    }
                    else
                    {
                        existing = new HoursPerWorkAssignmentModel()
                        {
                            Id_WorkAssignment = assignmentId,
                            WorkAssignmentName = timesheet.WorkAssignments.Title,
                            TotalHours = (timesheet.StopTime.Value - timesheet.StartTime.Value).TotalHours,
                            WorkComplete = timesheet.WorkComplete,
                            StartTime = timesheet.StartTime.Value,
                            StopTime = timesheet.StopTime.Value,
                            Comments = timesheet.Comments
                        };
                        model.Add(existing);
                    }
                }

                return View(model);
            }
            finally
            {
                entities.Dispose();
            }
        }

        public ActionResult HoursPerWorkAssignmentAsExcel()
        {
            // TODO: hae tiedot tietokannasta!
            //Muododostetaan data stingbuilderillä:
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto 
            csv.AppendLine("Matti;123,5");
            csv.AppendLine("Jesse;86,25");
            csv.AppendLine("Kaisa;99,00");

            // palautetaan CSV-tiedot selaimelle
            //Muutetaan string-builder tavutaulukoksi, jossa käytetään UTF8-merkistöä:
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString()); //merkkijono
            //File-rutiini, jossa käytetään tavutaulukkoa ja palautetaan MVC-controllerista tiedosto:
            return File(buffer, "text/csv", "Työtunnit.csv"); //MIME csv -tietotyyppi, tiedoston nimi
        }

        public ActionResult HoursPerWorkAssignmentAsExcel2()
        {
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto
            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Timesheets> allTimesheetsToday = (from ts in entities.Timesheets
                                                       where (ts.StartTime > today) &&
                                                       (ts.StartTime < tomorrow) &&
                                                       (ts.WorkComplete == true)
                                                       select ts).ToList();

                foreach (Timesheets timesheet in allTimesheetsToday)
                {
                    csv.AppendLine(timesheet.Id_Employee + ";" +
                        timesheet.StartTime + ";" + timesheet.StopTime + ";" + timesheet.Comments + ";");
                }
            }
            finally
            {
                entities.Dispose();
            }

            // palautetaan CSV-tiedot selaimelle
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "Työtunnit2.csv");
        }

        public ActionResult HoursPerWorkAssignmentAsExcelWeek()
        {
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto
            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                DateTime today = DateTime.Today;
                DateTime week = today.AddDays(7);

                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Timesheets> allTimesheetsWeek = (from ts in entities.Timesheets
                                                       where (ts.StartTime > today) &&
                                                       (ts.StartTime < week) &&
                                                       (ts.WorkComplete == true)
                                                       select ts).ToList();



                foreach (Timesheets timesheet in allTimesheetsWeek)
                {
                    csv.AppendLine(timesheet.Id_Employee + ";" +
                        timesheet.StartTime + ";" + timesheet.StopTime + ";" + timesheet.Comments + ";");
                }
            }
            finally
            {
                entities.Dispose();
            }

            // palautetaan CSV-tiedot selaimelle
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "Työtunnit3.csv");
        }

        public ActionResult HoursPerWorkAssignmentAsExcelDates()
        {
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto
            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                DateTime week = today.AddDays(7);      
                DateTime month = today.AddMonths(1);

                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Timesheets> allTimesheetsWeek = (from ts in entities.Timesheets
                                                      where (ts.StartTime > today) &&
                                                      (ts.StartTime < week) &&
                                                      (ts.WorkComplete == true)
                                                      select ts).ToList();



                foreach (Timesheets timesheet in allTimesheetsWeek)
                {
                    csv.AppendLine(timesheet.Id_Employee + ";" +
                        timesheet.StartTime + ";" + timesheet.StopTime + ";" + timesheet.Comments + ";");
                }
            }
            finally
            {
                entities.Dispose();
            }

            // palautetaan CSV-tiedot selaimelle
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "Työtunnit3.csv");
        }

        //public ActionResult GetTimesheetDates(String dates)
        //{
        //    GetTimesheetDatesModel model = new GetTimesheetDatesModel();
        //    MobileWorkDataEntities entities = new MobileWorkDataEntities();

        //    try
        //    {
        //        DateTime today = DateTime.Today;
        //        DateTime tomorrow = today.AddDays(1);
        //        DateTime week = today.AddDays(7);
        //        DateTime month = today.AddMonths(1);

        //        if (dates == "1")
        //        {
        //            model.Dates = (from ts in entities.Timesheets
        //                           where (ts.StartTime > today) &&
        //                                 (ts.StartTime < tomorrow) &&
        //                                  (ts.WorkComplete == true)
        //                           select ts.Title).ToArray();
        //        }
        //        else if (dates == "2")
        //        {
        //            model.Dates = (from ts in entities.Timesheets
        //                           where (ts.StartTime > today) &&
        //                                 (ts.StartTime < week) &&
        //                                  (ts.WorkComplete == true)
        //                           select ts.).ToArray();
        //        }
        //        else if (dates == "3")
        //        {
        //            model.Dates = (from ts in entities.Timesheets
        //                           where (ts.StartTime > today) &&
        //                                 (ts.StartTime < month) &&
        //                                  (ts.WorkComplete == true)
        //                           select ts.).ToArray();
        //        }

        //        else
        //        {
        //            model.Dates = (from ts in entities.Timesheets
        //                           select ts.).ToArray();
        //        }
        //    }
        //    finally
        //    {
        //        entities.Dispose();
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetTimesheetCounts(string onlyComplete)
        {
            ReportChartDataViewModel model = new ReportChartDataViewModel();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                model.Labels = (from wa in entities.WorkAssignments
                                orderby wa.Id_WorkAssignment
                                select wa.Title).ToArray();

                if (onlyComplete == "1")
                {
                    model.Counts = (from ts in entities.Timesheets
                                    where (ts.WorkComplete == true)
                                    orderby ts.Id_WorkAssignment
                                    group ts by ts.Id_WorkAssignment into grp
                                    select grp.Count()).ToArray();
                }
                else
                {
                    model.Counts = (from ts in entities.Timesheets
                                    orderby ts.Id_WorkAssignment
                                    group ts by ts.Id_WorkAssignment into grp
                                    select grp.Count()).ToArray();
                }
            }
            finally
            {
                entities.Dispose();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTimesheetCounts2(string onlyComplete)
        {
            ReportChartDataViewModel model = new ReportChartDataViewModel();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                model.Labels = (from wa in entities.WorkAssignments
                                orderby wa.Id_WorkAssignment
                                select wa.Title).ToArray();

                if (onlyComplete == "1")
                {
                    model.Counts = (from ts in entities.Timesheets
                                    where (ts.WorkComplete == true)
                                    orderby ts.Id_WorkAssignment
                                    group ts by ts.Id_WorkAssignment into grp
                                    select grp.Count()).ToArray();
                }
                else
                {
                    model.Counts = (from ts in entities.Timesheets
                                    orderby ts.Id_WorkAssignment
                                    group ts by ts.Id_WorkAssignment into grp
                                    select grp.Count()).ToArray();
                }
            }
            finally
            {
                entities.Dispose();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerCounts(string onlyComplete3)
        {
            ReportChartDataViewModel model = new ReportChartDataViewModel();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                model.Labels = (from wa in entities.WorkAssignments
                                orderby wa.Id_WorkAssignment
                                select wa.Title).ToArray();

                if (onlyComplete3 == "1")
                {
                    model.Counts = (from ts in entities.Timesheets
                                    where (ts.WorkComplete == true)
                                    orderby ts.Id_Customer
                                    group ts by ts.Id_Customer into grp
                                    select grp.Count()).ToArray();
                }
                else
                {
                    model.Counts = (from ts in entities.Timesheets
                                    orderby ts.Id_Customer
                                    group ts by ts.Id_Customer into grp
                                    select grp.Count()).ToArray();
                }
            }
            finally
            {
                entities.Dispose();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeCounts(string onlyComplete4)
        {
            ReportChartDataViewModel model = new ReportChartDataViewModel();

            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                model.Labels = (from wa in entities.WorkAssignments
                                orderby wa.Id_WorkAssignment
                                select wa.Title).ToArray();

                if (onlyComplete4 == "1")
                {
                    model.Counts = (from ts in entities.Timesheets
                                    where (ts.WorkComplete == true)
                                    orderby ts.Id_Employee
                                    group ts by ts.Id_Employee into grp
                                    select grp.Count()).ToArray();
                }
                else
                {
                    model.Counts = (from ts in entities.Timesheets
                                    orderby ts.Id_Employee
                                    group ts by ts.Id_Employee into grp
                                    select grp.Count()).ToArray();
                }
            }
            finally
            {
                entities.Dispose();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        
    }
}
