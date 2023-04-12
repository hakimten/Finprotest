using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finprotest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["id_user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }

        public ActionResult About()
        {
            if (Session["id_user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }

        public ActionResult Contact()
        {
            if (Session["id_user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
    }
}