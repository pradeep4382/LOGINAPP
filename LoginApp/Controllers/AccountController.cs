using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoginApp.Models;
using Dapper;
using WebMatrix.WebData;
using LoginApp.Filters;
using LoginApp.Repository;
using LoginApp.DataAccess;

namespace LoginApp.Controllers
{


    public class AccountController : Controller
    {
        IAccountData objIAccountData;

        public AccountController()
        {
            objIAccountData = new AccountData();
        }



       


        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View("SignIn");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginDetails login)
        {
            if (ModelState.IsValid)
            {



                using (var ctx = new LoginContext())
                {
                    var existingStudent = ctx.TestLogins.Where(s => s.UserName == login.UserName &&  s.Password==login.Password)
                                                            .FirstOrDefault<LoginDetails>();

                    if (existingStudent != null)
                    {
                        Session["Name"] = existingStudent.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Please enter valid Username and Password");
                        return View();
                    }
                }
            }
            return View();







        }





        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(LoginDetails register, string actionType)
        {
            if (actionType == "Save")
            {
                if (ModelState.IsValid)
                {

                    using (var ctx = new LoginContext())
                    {
                        ctx.TestLogins.Add(new LoginDetails()
                        {
                           
                            FirstName = register.FirstName,
                            LastName = register.LastName,
                            UserName=register.UserName,
                            Password=register.Password
                        });
                        ctx.SaveChanges();
                        Response.Redirect("~/account/login");
                    }

                   

                      
                    }

                }
                else
                {
                    ModelState.AddModelError("Error", "Please enter all details");
                }
                return View();

            }
           
        







        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            Response.Redirect("~/account/SignIn");
            return View();
        }

      


        



















        

        


       
       
    }
}
