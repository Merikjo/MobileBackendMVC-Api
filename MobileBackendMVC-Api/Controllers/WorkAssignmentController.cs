using MobileBackendMVC_Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeSheetMobXForm.Models;

namespace MobileBackendMVC_Api.Controllers
{
    public class WorkAssignmentController : ApiController
    {
        public string[] GetAll()
        {
            string[] assignmentNames = null;
            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();
            try
            {
                assignmentNames = (from wa in entities.WorkAssignments
                                   where (wa.Active == true)
                                   select wa.Title).ToArray();
            }
            finally
            {
                entities.Dispose();
            }

            return assignmentNames;
        }

        [HttpPost]
        public bool PostStatus(WorkAssignmentOperationModel input)
        {
            JohaMeriSQL5Entities entities = new JohaMeriSQL5Entities();
            try
            {
                WorkAssignments assignment = (from wa in entities.WorkAssignments
                                             where (wa.Active == true) &&
                                             (wa.Title == input.AssignmentTitle)
                                             select wa).FirstOrDefault();

                if (assignment == null)
                {
                    return false;
                }

                if (input.Operation == "Start")
                {
                    int assignmentId = assignment.Id_WorkAssignment;

                    Timesheets newEntry = new Timesheets()
                    {
                        Id_WorkAssignment = assignmentId,
                        StartTime = DateTime.Now,
                        WorkComplete = false,
                        Active = true,
                        CreatedAt = DateTime.Now
                    };
                    entities.Timesheets.Add(newEntry);
                }
                else if (input.Operation == "Stop")
                {
                    int assignmentId = assignment.Id_WorkAssignment;

                    Timesheets existing = (from ts in entities.Timesheets
                                          where (ts.Id_WorkAssignment == assignmentId) &&
                                          (ts.Active == true) && (ts.WorkComplete == false)
                                          orderby ts.StartTime descending
                                          select ts).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.StopTime = DateTime.Now;
                        existing.WorkComplete = true;
                        existing.LastModifiedAt = DateTime.Now;
                    }
                    else
                    {
                        return false;
                    }
                }

                entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            finally
            {
                entities.Dispose();
            }

            return true;
        }
    }
}
