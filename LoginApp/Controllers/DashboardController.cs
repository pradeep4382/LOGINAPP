using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginApp.Filters;

namespace LoginApp.Controllers
{
    
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        [MyExceptionHandler]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            return View();
        }



    }
}
