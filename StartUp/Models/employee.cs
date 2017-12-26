using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUp.Models
{
    public class employee : crudhandler
    {


        public string GetAllEmployee()
        {
            string querey;
            querey = "select * from Employee as e inner join Department as d on e.Departmentid = d.deptId";
            return this.executeQuerey(querey);
        }

        public string EmployeeUpdate(Hashtable columnArray, Hashtable whereArray)
        {
            return this.update("Employee", columnArray, whereArray);
        }



    }
}