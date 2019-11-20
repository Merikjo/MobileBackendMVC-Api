
using MobileBackendMVC_Api.DataAccess;
using MobileBackendMVC_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobileBackendMVC_Api.Controllers
{
    public class CustomerController : ApiController
    {
        public string[] GetAll()
        {
            string[] customerNames = null;
            MobileWorkDataEntities entities = new MobileWorkDataEntities();
            try
            {
                customerNames = (from c in entities.Customers
                                 where (c.Active == true)
                                 select c.CustomerName).ToArray();
            }
            finally
            {
                entities.Dispose();
            }

            return customerNames;
        }
    }
}
