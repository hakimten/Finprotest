using Finprotest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Finprotest.Controllers
{
    public class LoginController : Controller
    {
        string Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        //string Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        latihan1Entities entity = new latihan1Entities();
        //HakimEntities entity = new HakimEntities();
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

                return RedirectToAction("Dashboard", "Admin");
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
        [HttpPost]
        public ActionResult registeruser(FormCollection form)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
            //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            myConnection.Open();
            var name = form["name"].ToString();
            var username = form["username"].ToString();
            var phone = form["phone"].ToString();
            var password = form["password"].ToString();
            var pass = encrypt.GetMD5(password);
            var address = form["address"].ToString();
            string gender = form["gender"].ToString();
            //int group = 2;
            SqlCommand checktusername = new SqlCommand("select * from account_user where username_user = '" + username + "'", myConnection);
            SqlDataAdapter sd = new SqlDataAdapter(checktusername);
            DataTable dtt = new DataTable();
            sd.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
            }
            else
            {
                SqlConnection sqlconn = new SqlConnection(connectionString);
                string query7 = "INSERT INTO account_user(name_user, username_user, password_user, address_user, phone, gender_user, created_at, update_at)" +
                    " values (@name_user, @username_user, @password_user, @address_user, @phone, @gender_user, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";
                SqlCommand sqlcomm = new SqlCommand(query7, sqlconn);
                sqlconn.Open();
                sqlcomm.Parameters.AddWithValue("@name_user", name);
                sqlcomm.Parameters.AddWithValue("@username_user", username);
                sqlcomm.Parameters.AddWithValue("@password_user", pass);
                sqlcomm.Parameters.AddWithValue("@address_user", address);
                sqlcomm.Parameters.AddWithValue("@phone", phone);
                sqlcomm.Parameters.AddWithValue("@gender_user", gender);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }

            myConnection.Close();
            return RedirectToAction("LoginUser", "Login");
        }
        [HttpPost]
        public ActionResult registerOwner(FormCollection form)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
            //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            myConnection.Open();
            var name = form["name"].ToString();
            var username = form["username"].ToString();
            var phone = form["phone"].ToString();
            var password = form["password"].ToString();
            var pass = encrypt.GetMD5(password);
            var address = form["address"].ToString();
            string gender = form["gender"].ToString();
            //int group = 2;
            SqlCommand checktusername = new SqlCommand("select * from account_owner where username_owner = '" + username + "'", myConnection);
            SqlDataAdapter sd = new SqlDataAdapter(checktusername);
            DataTable dtt = new DataTable();
            sd.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
            }
            else
            {
                SqlConnection sqlconn = new SqlConnection(connectionString);
                string query7 = "INSERT INTO account_owner(name_owner, username_owner, password_owner, address_owner, phone, gender_owner, created_at, update_at)" +
                    " values (@name_owner, @username_owner, @password_owner, @address_owner, @phone, @gender_owner, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";
                SqlCommand sqlcomm = new SqlCommand(query7, sqlconn);
                sqlconn.Open();
                sqlcomm.Parameters.AddWithValue("@name_owner", name);
                sqlcomm.Parameters.AddWithValue("@username_owner", username);
                sqlcomm.Parameters.AddWithValue("@password_owner", pass);
                sqlcomm.Parameters.AddWithValue("@address_owner", address);
                sqlcomm.Parameters.AddWithValue("@phone", phone);
                sqlcomm.Parameters.AddWithValue("@gender_owner", gender);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }

            myConnection.Close();
            return RedirectToAction("LoginOwner", "Login");
        }
    }

}