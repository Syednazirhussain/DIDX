using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StartUp.Models
{
    public class Configure
    {
        public static String Connection()
        {
            return ConfigurationManager.ConnectionStrings["DIDX"].ConnectionString; 
        }


        public static SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection(Configure.Connection());
            return con;
        }
    }
}