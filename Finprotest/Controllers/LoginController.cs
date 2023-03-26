using Finprotest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Finprotest.Controllers
{
    public class LoginController : Controller
    {
        latihan1Entities entity = new latihan1Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginAdmin()
        {
            if (Session["username_admin"] != null)
            {
                Session["username_admin"].ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoginAdmin(account_admin credentails)
        {

            string encrypt1 = encrypt.GetMD5(credentails.password_admin);
            bool userExist = entity.account_admin.Any(x => x.username_admin == credentails.username_admin && x.password_admin == encrypt1);
            account_admin u = entity.account_admin.FirstOrDefault(x => x.username_admin == credentails.username_admin && x.password_admin == encrypt1);


            if (userExist)
            {
                Session["username_admin"] = u.username_admin.ToString();
                Session["name_admin"] = u.name_admin.ToString();
                Session["id_admin"] = u.id_admin;

                FormsAuthentication.SetAuthCookie(u.username_admin, false);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "udah ada");
            if (Session["username_admin"] == null)
            {
                return RedirectToAction("LoginAdmin", "Login");
            }
            return View();
        }
        public ActionResult LoginUser()
        {
            if (Session["username_user"] != null)
            {
                Session["username_user"].ToString();
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoginUser(account_user credentails)
        {

            string encrypt1 = encrypt.GetMD5(credentails.password_user);
            bool userExist = entity.account_user.Any(x => x.username_user == credentails.username_user && x.password_user == encrypt1);
            account_user u = entity.account_user.FirstOrDefault(x => x.username_user == credentails.username_user && x.password_user == encrypt1);


            if (userExist)
            {
                Session["username_user"] = u.username_user.ToString();
                Session["name_user"] = u.name_user.ToString();
                Session["id_user"] = u.id_user;

                FormsAuthentication.SetAuthCookie(u.username_user, false);

                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", "udah ada");
            if (Session["username_user"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            return View();
        }
        public ActionResult LoginOwner()
        {
            if (Session["username_user"] != null)
            {
                Session["username_user"].ToString();
                return RedirectToAction("Index", "Owner");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoginOwner(account_owner credentails)
        {

            string encrypt1 = encrypt.GetMD5(credentails.password_owner);
            bool userExist = entity.account_owner.Any(x => x.username_owner == credentails.username_owner && x.password_owner == encrypt1);
            account_owner u = entity.account_owner.FirstOrDefault(x => x.username_owner == credentails.username_owner && x.password_owner == encrypt1);


            if (userExist)
            {
                Session["username_owner"] = u.username_owner.ToString();
                Session["name_owner"] = u.name_owner.ToString();
                Session["id_owner"] = u.id_owner;

                FormsAuthentication.SetAuthCookie(u.username_owner, false);

                return RedirectToAction("Index", "Owner");
            }
            ModelState.AddModelError("", "udah ada");
            if (Session["username_user"] == null)
            {
                return RedirectToAction("LoginOwner", "Login");
            }
            return View();
        }
    }
}