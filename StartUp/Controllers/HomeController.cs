using Newtonsoft.Json;
using StartUp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace StartUp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public string Register(string username,string email,string password)
        {
            Hashtable Columns = new Hashtable();
            Columns.Add("username", username);
            Columns.Add("email",email);
            Columns.Add("password",password);

            account account = new account();

            return account.SignUp("Users", Columns);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()] 
        public string Login(string email,string password)
        {
            account account = new account();
            return account.Login(email, password);
        }





    }




}