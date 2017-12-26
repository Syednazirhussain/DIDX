using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace StartUp.Models
{
    public class account : crudhandler
    {
        public string SignUp(string tablename,Hashtable Columns)
        {
            List<Tuple<string, string>> columns = new List<Tuple<string, string>>();
            foreach (string item in Columns.Keys)
            {
                columns.Add(Tuple.Create(item, "\'" + Columns[item] + "\'"));
            }
            return this.insert("Users",columns);
        }

        public string Login(string email,string password)
        {

            
            List<Tuple<string,string>> columns = new List<Tuple<string,string>>();

            columns.Add(Tuple.Create("email","\'"+email+"\'"));
            columns.Add(Tuple.Create("password","\'"+password+"\'"));

            return this.select("Users",columns);

        }


    }
}