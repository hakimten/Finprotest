using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finprotest.Controllers
{
    public class OwnerController : Controller
    {
        // GET: Owner
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Product_soldout()
        {
            return View();
        }
        public ActionResult Order_masuk()
        {
            return View();
        }
        public ActionResult History_order()
        {
            return View();
        }
        public ActionResult Profil()
        {
            return View();
        }
    }
}