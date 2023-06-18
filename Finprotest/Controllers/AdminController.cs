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

namespace Finprotest.Controllers
{
    public class AdminController : Controller
    {
        //string Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        string Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["id_admin"] != null)
            {
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                // display product owner 
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name, t5.username_owner FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.id_owner = t5.id_owner WHERE t1.product_status = 'B'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<Adminclass> uc = new List<Adminclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        Adminclass uc10 = new Adminclass();
                        uc10.RP = Convert.ToString(dr["RP"]);
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.Toko_name = Convert.ToString(dr["Toko_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.address_owner = Convert.ToString(dr["address_owner"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.username_owner = Convert.ToString(dr["username_owner"]);
                        uc10.Product_id = Convert.ToInt32(dr["Product_id"]);
                        uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);
                        uc10.id_owner = Convert.ToInt32(dr["id_owner"]);
                        uc10.product_harga = Convert.ToInt32(dr["product_harga"]);
                        uc10.Product_stock = Convert.ToInt32(dr["Product_stock"]);
                        uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);
                        uc10.Toko_id = Convert.ToInt32(dr["Toko_id"]);

                        uc.Add(uc10);
                    }
                }
                String sqlquery3 = "SELECT * FROM account_owner";
                SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
                SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
                DataTable ds3 = new DataTable();
                sda3.Fill(ds3);
                List<Adminclass> uc2 = new List<Adminclass>();
                {

                    foreach (DataRow dr in ds3.Rows)
                    {
                        Adminclass uc10 = new Adminclass();
                        uc10.address_owner = Convert.ToString(dr["address_owner"]);
                        uc10.username_owner = Convert.ToString(dr["username_owner"]);
                        uc10.name_owner = Convert.ToString(dr["name_owner"]);
                        uc10.id_owner = Convert.ToInt32(dr["id_owner"]);

                        uc2.Add(uc10);
                    }
                } 
                String sqlquery4 = "SELECT * FROM account_user WHERE acc_status = 'B'";
                SqlCommand sqlcomm4 = new SqlCommand(sqlquery4, sqlconn);
                SqlDataAdapter sda4 = new SqlDataAdapter(sqlcomm4);
                DataTable ds4 = new DataTable();
                sda4.Fill(ds4);
                List<Adminclass> uc3 = new List<Adminclass>();
                {

                    foreach (DataRow dr in ds4.Rows)
                    {
                        Adminclass uc10 = new Adminclass();
                        uc10.name_user = Convert.ToString(dr["name_user"]);
                        uc10.email_user = Convert.ToString(dr["email_user"]);
                        uc10.username_user = Convert.ToString(dr["username_user"]);
                        uc10.id_user = Convert.ToInt32(dr["id_user"]);

                        uc3.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                ViewBag.uc3 = uc3;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdmin", "Login");
            }
        }
        public ActionResult Approve(FormCollection form)
        {
            SqlConnection myConnection = new SqlConnection();
            string idalmt = form["idalmt"];

            myConnection.ConnectionString = Mainconn;
            string Query3 = "UPDATE Product_owner SET product_status = 'A' WHERE Product_id = @Product_id";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@Product_id", idalmt);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            return RedirectToAction("Dashboard");
        }
        public ActionResult Approve2(FormCollection form, string Subject)
        {
            SqlConnection myConnection = new SqlConnection();
            string idalmt = form["idalmt"];
            string email = form["email"];

            myConnection.ConnectionString = Mainconn;
            string Query3 = "UPDATE account_user SET acc_status = 'A' WHERE id_user = @id_user";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@id_user", idalmt);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            //EMAIL 
            string memberemail = form["email"];
            //string memberemail2 = "tengkuikhmanul.hakim@mattel.com";
            //string memberemail3 = "rifkymuhammad.juliawan@mattel.com";
            Subject = "STATUS ACCOUNT USER";
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
            mail.Body = PopulateBodynewtoy(email);
            //mail.Subject = "Superior1 #" + PaymentRequest_ID + "";
            mail.Subject = "STATUS ACCOUNT USER";
            mail.IsBodyHtml = true;
            smtpClient.Send(mail);

            // Show account activation modal
            TempData["ActivationMessage"] = "Account requested, wait for PIC to activate the account";
            TempData["ShowActivationModal"] = true;
            return RedirectToAction("Dashboard");
        }
        private string PopulateBodynewtoy(string email)
        {

            //subject = "file already uploaded";



            var today = DateTime.Today.ToString("dd-MMM-yyyy");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/email/HtmlPage2.html")))
            {
                body = reader.ReadToEnd();
            }



            body = body.Replace("{email}", email);
            //body = body.Replace("{username}", username);
            //body = body.Replace("{keprinting}", keprinting);
            //body = body.Replace("{name}", name);
            //body = body.Replace("{divisi}", divisi);
            //body = body.Replace("{reqfor}", reqfor);



            return body;
        }
        public ActionResult Delete(FormCollection form)
        {
            SqlConnection myConnection = new SqlConnection();
            string noresi = form["noresi"];
            string idalmt = form["idalmt"];

            myConnection.ConnectionString = Mainconn;
            string Query3 = "UPDATE Estimasi_waktu SET no_resi = @no_resi WHERE cout_id = @cout_id";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@cout_id", idalmt);
                sqlmethod.Parameters.AddWithValue("@no_resi", noresi);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            return RedirectToAction("Dashboard");
        }
    }
}