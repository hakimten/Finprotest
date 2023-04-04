using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finprotest.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            //if (Session["id_admin"] != null)
            //{
                
            //}
            //else
            //{
            //    return RedirectToAction("LoginAdmin", "Login");
            //}
            return View();
        }
    }
}