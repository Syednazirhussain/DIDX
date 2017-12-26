using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StartUp.Models
{
    public class department : crudhandler
    {
        public string getAllDepartment()
        {

            return this.select("Department");

        }


        public string DepartmentAdd(string[] param)
        {

            Dictionary<int, List<string>> columnsValues = new Dictionary<int, List<string>>();
            for (int i = 0; i < param.Length ; i++)
            {
                columnsValues.Add(i, new List<string>() { "\'" + param[i] + "\'" });
            }
            string[] insertColumn = { "deptName" };

            return this.insert("Department",insertColumn,columnsValues);

            //return this.insert("Department", Columns);
            //string[] columns = {"empName","Gender","Departmentid" };

            //Dictionary<int, List<string>> columnsValues = new Dictionary<int, List<string>>();
            //columnsValues.Add(0, new List<string>() { "\'Nazir\'", "\'male\'", "1" });
            //columnsValues.Add(1, new List<string>() { "\'Hamza\'", "\'male\'", "2" });
            //columnsValues.Add(2, new List<string>() { "\'Taha\'", "\'male\'", "2" });
            //columnsValues.Add(3, new List<string>() { "\'Onais\'", "\'male\'", "3" });

            //return this.insert("Employee", columns, columnsValues);

        }

        public string DepartmentUpdate(string deptId,string deptName)
        {
            Hashtable columnArray = new Hashtable();
            Hashtable whereArray = new Hashtable();

            columnArray.Add("deptName", "\'" + deptName + "\'");
            whereArray.Add("deptId",deptId);

            return this.update("Department", columnArray, whereArray);
        }

        public string DepartmentDelete(int ID)
        {
            Hashtable whereClause = new Hashtable();
            whereClause.Add("deptId",ID);
            return this.delete("Department", whereClause);
 
        }


    }
}