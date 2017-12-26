using Newtonsoft.Json.Linq;
using StartUp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StartUp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            
            return View();
        }

        public string PageRender()
        {
            employee emp = new employee();
            return emp.GetAllEmployee();
        }

        [HttpPost]
        public string UpdateEmployee(string json)
        {

            var obj = JObject.Parse(json);
            Hashtable updateColumns = new Hashtable();
            Hashtable whereClause = new Hashtable();
            foreach (JProperty x in (JToken)obj)
            {
                if (string.Compare(x.Name, "empId") == 0)
                {
                    whereClause.Add(x.Name, x.Value);
                }
                else
                {
                    updateColumns.Add(x.Name, "\'" + x.Value + "\'");
                }
            }

            employee emp = new employee();
            return emp.EmployeeUpdate(updateColumns,whereClause);
        }
    }
}