using Finprotest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Finprotest.Controllers
{
    public class LoginController : Controller
    {
        //string Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        string Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        //latihan1Entities entity = new latihan1Entities();
        HakimEntities entity = new HakimEntities();
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
        public ActionResult registeruser(FormCollection form, string Subject)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
            //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            myConnection.Open();
            var name = form["name"].ToString();
            var username = form["username"].ToString();
            var email = form["email"].ToString();
            var phone = form["phone"].ToString();
            var password = form["password"].ToString();
            var pass2 = encrypt.GetMD5(password);
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
                string query7 = "INSERT INTO account_user(name_user, username_user, email_user, password_user, address_user, phone, gender_user, acc_status, created_at, update_at)" +
                    " values (@name_user, @username_user, @email_user, @password_user, @address_user, @phone, @gender_user, 'B', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";
                SqlCommand sqlcomm = new SqlCommand(query7, sqlconn);
                sqlconn.Open();
                sqlcomm.Parameters.AddWithValue("@name_user", name);
                sqlcomm.Parameters.AddWithValue("@username_user", username);
                sqlcomm.Parameters.AddWithValue("@password_user", pass2);
                sqlcomm.Parameters.AddWithValue("@address_user", address);
                sqlcomm.Parameters.AddWithValue("@phone", phone);
                sqlcomm.Parameters.AddWithValue("@email_user", email);
                sqlcomm.Parameters.AddWithValue("@gender_user", gender);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }
            //EMAIL 
            string memberemail = "tengkuikhmanulhakim2002@gmail.com";
            //string memberemail2 = "tengkuikhmanul.hakim@mattel.com";
            //string memberemail3 = "rifkymuhammad.juliawan@mattel.com";
            Subject = "REQUEST REGISTER ACCOUNT";
            //var emailfrom = "tengkuikhmanul1202@gmail.com";
            //var pass = "Medan2019";
            var emailfrom = "PTMIProddevSystem@mattel.com";
            var pass = "Tgl10042000.";
            //string keprinting = "10.35.101.46/SewingRequest/Index";


            SmtpClient smtpClient = new SmtpClient();
            //smtpClient.Host = "smtp.gmail.com";
            //smtpClient.Port = 587;
            smtpClient.Host = "10.1.30.16";
            smtpClient.Port = 25;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailfrom, pass);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;



            MailMessage mail = new MailMessage();
            //var to = usermail;
            //Debug.WriteLine($"");
            var from = emailfrom;
            //var getidemp = requestor.NamaLengkap.ToString();
            //var getidemp = row.ID_Emp.ToString();



            string[] Multi = memberemail.Split(',');
            foreach (string Multimail in Multi)
            {
                mail.To.Add(new MailAddress(Multimail));
            }
            //mail.CC.Add(new MailAddress(ccemail));
            //string[] Multi2 = memberemail2.Split(',');
            //foreach (string Multimail2 in Multi2)
            //{
            //    mail.To.Add(new MailAddress(Multimail2));
            //}
            //string[] Multi3 = memberemail3.Split(',');
            //foreach (string Multimail3 in Multi3)
            //{
            //    mail.To.Add(new MailAddress(Multimail3));
            //}
            //mail.To.Add(new MailAddress(ToMail));
            mail.From = new MailAddress(from);
            mail.Body = PopulateBodynewtoy(username);
            //mail.Subject = "Superior1 #" + PaymentRequest_ID + "";
            mail.Subject ="APPROVE ACCOUNT USER";
            mail.IsBodyHtml = true;
            smtpClient.Send(mail);

            // Show account activation modal
            TempData["ActivationMessage"] = "Account requested, wait for PIC to activate the account";
            TempData["ShowActivationModal"] = true;
            myConnection.Close();
            return RedirectToAction("LoginUser", "Login");
        }
        private string PopulateBodynewtoy(string username)
        {

            //subject = "file already uploaded";



            var today = DateTime.Today.ToString("dd-MMM-yyyy");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/email/HtmlPage1.html")))
            {
                body = reader.ReadToEnd();
            }



            body = body.Replace("{username}", username);
            //body = body.Replace("{username}", username);
            //body = body.Replace("{keprinting}", keprinting);
            //body = body.Replace("{name}", name);
            //body = body.Replace("{divisi}", divisi);
            //body = body.Replace("{reqfor}", reqfor);



            return body;
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
        public ActionResult changepassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changepassword2(FormCollection form, string Subject)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
            //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            myConnection.Open();
            string email = form["email"];
            SqlCommand checktusername = new SqlCommand("select * from account_user where email_user = '" + email + "'", myConnection);
            SqlDataAdapter sd = new SqlDataAdapter(checktusername);
            DataTable dtt = new DataTable();
            sd.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
                //EMAIL 
                string memberemail = form["email"];
                //string memberemail2 = "tengkuikhmanul.hakim@mattel.com";
                //string memberemail3 = "rifkymuhammad.juliawan@mattel.com";
                Subject = "CHANGE PASSWORD";
                //var emailfrom = "tengkuikhmanul1202@gmail.com";
                //var pass = "Medan2019";
                var emailfrom = "PTMIProddevSystem@mattel.com";
                var pass = "Tgl10042000.";
                string keprinting = "https://localhost:44349/Login/changepassword3";


                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = "smtp.gmail.com";
                //smtpClient.Port = 587;
                smtpClient.Host = "10.1.30.16";
                smtpClient.Port = 25;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailfrom, pass);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = false;



                MailMessage mail = new MailMessage();
                //var to = usermail;
                //Debug.WriteLine($"");
                var from = emailfrom;
                //var getidemp = requestor.NamaLengkap.ToString();
                //var getidemp = row.ID_Emp.ToString();



                string[] Multi = memberemail.Split(',');
                foreach (string Multimail in Multi)
                {
                    mail.To.Add(new MailAddress(Multimail));
                }
                //mail.CC.Add(new MailAddress(ccemail));
                //string[] Multi2 = memberemail2.Split(',');
                //foreach (string Multimail2 in Multi2)
                //{
                //    mail.To.Add(new MailAddress(Multimail2));
                //}
                //string[] Multi3 = memberemail3.Split(',');
                //foreach (string Multimail3 in Multi3)
                //{
                //    mail.To.Add(new MailAddress(Multimail3));
                //}
                //mail.To.Add(new MailAddress(ToMail));
                mail.From = new MailAddress(from);
                mail.Body = PopulateBodynewtoy2(email, keprinting);
                //mail.Subject = "Superior1 #" + PaymentRequest_ID + "";
                mail.Subject = "CHANGE PASSWORD";
                mail.IsBodyHtml = true;
                smtpClient.Send(mail);

                // Show account activation modal
                TempData["ActivationMessage"] = "Account requested, wait for PIC to activate the account";
                TempData["ShowActivationModal"] = true;
            }
            else
            {
                return RedirectToAction("validation");
            }
            myConnection.Close();
            return RedirectToAction("LoginUser");
        }
        private string PopulateBodynewtoy2(string email, string keprinting)
        {

            //subject = "file already uploaded";



            var today = DateTime.Today.ToString("dd-MMM-yyyy");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/email/HtmlPage3.html")))
            {
                body = reader.ReadToEnd();
            }



            body = body.Replace("{email}", email);
            //body = body.Replace("{username}", username);
            body = body.Replace("{keprinting}", keprinting);
            //body = body.Replace("{name}", name);
            //body = body.Replace("{divisi}", divisi);
            //body = body.Replace("{reqfor}", reqfor);



            return body;
        }
        public ActionResult changepassword3()
        {
            return View();
        } 
        public ActionResult changepassword4(FormCollection form)
        {
            SqlConnection myConnection = new SqlConnection();
            string email = form["email"];
            string username = form["username"];
            string password = form["password"];
            var pass = encrypt.GetMD5(password);
            myConnection.ConnectionString = Mainconn;
            string Query3 = "UPDATE account_user SET password_user = @password_user WHERE username_user = @username_user";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@username_user", username);
                sqlmethod.Parameters.AddWithValue("@password_user", pass);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            return RedirectToAction("LoginUser");
        }
        public ActionResult validation()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }

}