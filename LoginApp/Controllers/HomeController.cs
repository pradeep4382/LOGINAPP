using LoginApp.DataAccess;
using LoginApp.Filters;
using LoginApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]

        public ActionResult ChngPassword()
        {
            return View();
        }

        [HttpPost]
        [MyExceptionHandler]
        [ValidateAntiForgeryToken]
        public ActionResult ChngPassword(LoginDetails login)
        {
            if (ModelState.IsValid)
            {

                using (var ctx = new LoginContext())
                {
                    var existingStudent = ctx.TestLogins.Where(s => s.UserName == Convert.ToString(Session["Name"]))
                                                            .FirstOrDefault<LoginDetails>();

                    if (existingStudent != null)
                    {
                        if (existingStudent.Password != login.Password)
                        {
                            ModelState.AddModelError("Error", "Passwords are not same");
                            return View();

                        }
                        else
                        {
                            existingStudent.Password = login.Confirmpassword;
                            ctx.SaveChanges();
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Please enter valid Username and Password");
                        return View();
                    }
                }




            }
            else
            {
                ModelState.AddModelError("Error", "Fill on Fields");
            }


            return View();
        }


        public ActionResult Edit()
        {
          
            return View();
        }


        [HttpPost]
        public ActionResult Edit(ChangePassword login1)
        {

            if (ModelState.IsValid)
            {
                string Username = Convert.ToString(Session["Name"]);
                using (var ctx = new LoginContext())
                {
                    var existingStudent = ctx.TestLogins.Where(s => s.UserName == Username)
                                                           .FirstOrDefault<LoginDetails>();


                    if (existingStudent != null)
                    {
                        if (existingStudent.Password != login1.Password)
                        {
                            ModelState.AddModelError("Error", "Passwords are not same");
                            return View();

                        }
                        else
                        {
                            existingStudent.Password = login1.ChngePassword;
                            ctx.SaveChanges();
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Please enter valid Username and Password");
                        return View();
                    }
                }
            }
                    return RedirectToAction("Index");
        }

    }
}
