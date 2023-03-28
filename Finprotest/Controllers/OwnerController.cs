using Finprotest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finprotest.Controllers
{
    public class OwnerController : Controller
    {
        //String Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        String Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        // GET: Owner
        public ActionResult Index()
        {
            if (Session["id_owner"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult Product()
        {
            if (Session["id_owner"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult Product_soldout()
        {
            if (Session["id_owner"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult Order_masuk()
        {
            if (Session["id_owner"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult History_order()
        {
            if (Session["id_owner"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult Profil()
        {
            if (Session["id_owner"] != null)
            {
                string SessionName = Session["id_owner"].ToString();
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "SELECT t1.Toko_id, t1.Toko_name, t1.Toko_foto, t1.id_owner, t1.Toko_long, t1.Toko_lat, t1.Toko_nohp, t1.Toko_bank, Toko_norek, t1.Toko_status, t2.name_owner, t2.username_owner, t2.address_owner FROM Toko_Profil t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner WHERE t1.id_owner = '" + SessionName +"'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<Ownerclass> uc = new List<Ownerclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        Ownerclass uc10 = new Ownerclass();
                        uc10.Toko_long = Convert.ToString(dr["Toko_long"]);
                        uc10.Toko_lat = Convert.ToString(dr["Toko_lat"]);
                        uc10.Toko_foto = Convert.ToString(dr["Toko_foto"]);
                        uc10.Toko_name = Convert.ToString(dr["Toko_name"]);
                        uc10.Toko_nohp = Convert.ToString(dr["Toko_nohp"]);
                        uc10.Toko_bank = Convert.ToString(dr["Toko_bank"]);
                        uc10.Toko_norek = Convert.ToString(dr["Toko_norek"]);
                        uc10.address_owner = Convert.ToString(dr["address_owner"]);
                        uc10.username_owner = Convert.ToString(dr["username_owner"]);
                        uc10.name_owner = Convert.ToString(dr["name_owner"]);
                        uc10.Toko_id = Convert.ToInt32(dr["Toko_id"]);
                        uc10.id_owner = Convert.ToInt32(dr["id_owner"]);

                        uc.Add(uc10);
                    }
                }
                string sqlquery2 = "SELECT COUNT(Toko_id) AS REVISI FROM Toko_Profil WHERE id_owner = '" + SessionName + "'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<Ownerclass> uc2 = new List<Ownerclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        Ownerclass uc10 = new Ownerclass();
                        uc10.REVISI = Convert.ToString(dr["REVISI"]);

                        uc2.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult AddProfilOwner(FormCollection form, HttpPostedFileBase profilpicture)
        {
            if (Session["id_owner"] != null || Session["username_owner"] != null)
            {
                SqlConnection myConnection = new SqlConnection();
                string SessionName = Session["id_owner"].ToString();
                string SessionName2 = Session["username_owner"].ToString();
                string storename = form["storename"];
                string banktype = form["banktype"];
                string norekening = form["norekening"];
                string nohp = form["nohp"];
                string longitude = form["longitude"];
                string lattitude = form["lattitude"];

                string sewnPatternImage = "ProfilPicture" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(profilpicture.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/Profil/"), sewnPatternImage);
                profilpicture.SaveAs(filePathsewnPattern);

                myConnection.ConnectionString = Mainconn;
                string Query3 = "INSERT INTO Toko_Profil (Toko_name, Toko_foto, Kategori_ID, username_owner, id_owner, Toko_long, Toko_lat, Toko_nohp, Toko_bank, Toko_norek, Toko_status) VALUES (@Toko_name, @Toko_foto, '1', @username_owner, @id_owner, @Toko_long, @Toko_lat, @Toko_nohp, @Toko_bank, @Toko_norek, 'A')";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
                {
                    sqlmethod.Parameters.AddWithValue("@Toko_name", storename);
                    sqlmethod.Parameters.AddWithValue("@Toko_foto", sewnPatternImage);
                    sqlmethod.Parameters.AddWithValue("@username_owner", SessionName2);
                    sqlmethod.Parameters.AddWithValue("@id_owner", SessionName);
                    sqlmethod.Parameters.AddWithValue("@Toko_long", longitude);
                    sqlmethod.Parameters.AddWithValue("@Toko_lat", lattitude);
                    sqlmethod.Parameters.AddWithValue("@Toko_nohp", nohp);
                    sqlmethod.Parameters.AddWithValue("@Toko_bank", banktype);
                    sqlmethod.Parameters.AddWithValue("@Toko_norek", norekening);
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                return RedirectToAction("Profil");
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult EditProfilOwner(FormCollection form, HttpPostedFileBase profilpicture)
        {
            if (Session["id_owner"] != null || Session["username_owner"] != null)
            {
                SqlConnection myConnection = new SqlConnection();
                string SessionName = Session["id_owner"].ToString();
                string SessionName2 = Session["username_owner"].ToString();
                string storename = form["storename"];
                string banktype = form["banktype"];
                string norekening = form["norekening"];
                string nohp = form["nohp"];
                string longitude = form["longitude"];
                string lattitude = form["lattitude"];

                string sewnPatternImage = "ProfilPicture" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(profilpicture.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/Profil/"), sewnPatternImage);
                profilpicture.SaveAs(filePathsewnPattern);

                myConnection.ConnectionString = Mainconn;
                string Query3 = "INSERT INTO Toko_Profil (Toko_name, Toko_foto, Kategori_ID, username_owner, id_owner, Toko_long, Toko_lat, Toko_nohp, Toko_bank, Toko_norek, Toko_status) VALUES (@Toko_name, @Toko_foto, '1', @username_owner, @id_owner, @Toko_long, @Toko_lat, @Toko_nohp, @Toko_bank, @Toko_norek, 'A')";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
                {
                    sqlmethod.Parameters.AddWithValue("@Toko_name", storename);
                    sqlmethod.Parameters.AddWithValue("@Toko_foto", sewnPatternImage);
                    sqlmethod.Parameters.AddWithValue("@username_owner", SessionName2);
                    sqlmethod.Parameters.AddWithValue("@id_owner", SessionName);
                    sqlmethod.Parameters.AddWithValue("@Toko_long", longitude);
                    sqlmethod.Parameters.AddWithValue("@Toko_lat", lattitude);
                    sqlmethod.Parameters.AddWithValue("@Toko_nohp", nohp);
                    sqlmethod.Parameters.AddWithValue("@Toko_bank", banktype);
                    sqlmethod.Parameters.AddWithValue("@Toko_norek", norekening);
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                return RedirectToAction("Profil");
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
    }
}