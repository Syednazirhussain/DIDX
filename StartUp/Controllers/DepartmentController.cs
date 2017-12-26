using StartUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StartUp.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {

            return View();
        }

        public string PageRender()
        {
            department dept = new department();
            return dept.getAllDepartment();
        }

        [HttpPost]
        public string AddDepartments(string[] function_param)
        {
            if (function_param != null && function_param.Length > 0)
            {
                department dept = new department();
                return dept.DepartmentAdd(function_param);
                
            }

            return null;
        }

        [HttpPost]
        public string UpdateDepartment(string deptId,string deptName)
        {
            department dept = new department();
            return dept.DepartmentUpdate(deptId, deptName);
        }

        [HttpPost]
        public string DeleteDepartment(int ID)
        {
            department dept = new department();

            return dept.DepartmentDelete(ID);
        }


    }
}