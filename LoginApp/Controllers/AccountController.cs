﻿using System;
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
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                bool success =  WebSecurity.Login(login.username, login.password, false);
                var UserID = GetUserID_By_UserName(login.username);
                var LoginType = GetRoleBy_UserID(Convert.ToString(UserID));

                if (success == true)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(LoginType)))
                    {
                        ModelState.AddModelError("Error", "Rights to User are not Provide Contact to Admin");
                        return View(login);
                    }
                    else
                    {
                        Session["Name"] = login.username;
                        Session["UserID"] = UserID;
                        Session["LoginType"] = LoginType;

                        if (Roles.IsUserInRole(login.username, "Admin"))
                        {
                            return RedirectToAction("AdminDashboard", "Dashboard");
                        }
                        else
                        {
                            return RedirectToAction("UserDashboard", "Dashboard");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Please enter valid Username and Password");
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Username and Password");
            }
            return View(login);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register register, string actionType)
        {
            if (actionType == "Save")
            {
                if (ModelState.IsValid)
                {
                    if (!WebSecurity.UserExists(register.username))
                    {
                        WebSecurity.CreateUserAndAccount(register.username, register.password,
                            new { FullName = register.FullName, EmailID = register.EmailID });
                        Roles.AddUserToRole(register.username, "Admin");
                        Response.Redirect("~/account/login");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Please enter all details");
                }
                return View();

            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        [MyExceptionHandler]
        public ActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [MyExceptionHandler]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(Role role)
        {
            if (ModelState.IsValid)
            {
                if (Roles.RoleExists(role.RoleName))
                {
                    ModelState.AddModelError("Error", "Rolename already exists");
                    return View(role);
                }
                else
                {
                    Roles.CreateRole(role.RoleName);
                    return RedirectToAction("RoleIndex", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Username and Password");
            }
            return View(role);
        }



        [MyExceptionHandler]
        public ActionResult RoleIndex()
        {
            var roles = objIAccountData.GetRoles();
            return View(roles);
        }

        [MyExceptionHandler]
        public ActionResult RoleDelete(string RoleName)
        {
            Roles.DeleteRole(RoleName);
            return RedirectToAction("RoleIndex", "Account");
        }

        [HttpPost]
        [MyExceptionHandler]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
                SelectList list = new SelectList(Roles.GetAllRoles());
                ViewBag.Roles = list;
            }
            return View("RoleAddToUser");
        }

        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            Response.Redirect("~/account/login");
            return View();
        }

        [HttpGet]
        [MyExceptionHandler]
        public ActionResult Changepassword()
        {
            return View(new ChangepasswordVM());
        }

        [HttpPost]
        [MyExceptionHandler]
        [ValidateAntiForgeryToken]
        public ActionResult Changepassword(ChangepasswordVM VM)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(Convert.ToString(Session["Name"])))
                {
                    ModelState.AddModelError("Error", "UserName ");

                }
                else
                {
                    //var token = WebSecurity.GeneratePasswordResetToken(Convert.ToString(Session["Name"]));
                    //WebSecurity.ResetPassword(token, VM.password);
                    //ViewBag = "Password Changed";

                    var value = WebSecurity.ChangePassword(Session["Name"].ToString(), VM.OldPassword, VM.Newpassword);

                    if (value == false)
                    {
                        ModelState.AddModelError("Error", "Incorrect Old Password");
                        return View(VM);
                    }
                    else
                    {
                        ViewBag.ResultMessage = "Password Changed Successfully";
                    }

                }
            }
            else
            {
                ModelState.AddModelError("Error", "Fill on Fields");
            }
            return View(VM);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AllRegisterUserDetails()
        {
            var Users = objIAccountData.GetAllUsers();
            return View(Users);
        }

        [NonAction]
        public string GetRoleBy_UserID(string UserId)
        {
            return objIAccountData.GetRoleByUserID(UserId);
        }

        [NonAction]
        public string GetUserID_By_UserName(string UserName)
        {
            return objIAccountData.GetUserID_By_UserName(UserName);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CheckUserNameExists(string username)
        {
            bool UserExists = false;

            try
            {
                var nameexits = objIAccountData.Get_checkUsernameExits(username);

                if (string.Equals(nameexits, "1"))
                {
                    UserExists = true;
                }
                else
                {
                    UserExists = false;
                }
                return Json(!UserExists, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NonAction]
        public List<SelectListItem> GetAll_Roles()
        {
            List<SelectListItem> listrole = new List<SelectListItem>();

            listrole.Add(new SelectListItem { Text = "Select", Value = "0" });

            foreach (var item in Roles.GetAllRoles())
            {
                listrole.Add(new SelectListItem { Text = item, Value = item });
            }

            return listrole;
        }

        [NonAction]
        public List<SelectListItem> GetAll_Users()
        {
            var Userlist = objIAccountData.GetAllUsers();
            List<SelectListItem> listuser = new List<SelectListItem>();
            listuser.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var item in Userlist)
            {
                listuser.Add(new SelectListItem { Text = item.UserName, Value = item.Id });
            }

            return listuser;
        }

    }
}
