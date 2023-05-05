using Finprotest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finprotest.Controllers
{
    public class AdminController : Controller
    {
        string Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        //string Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["id_admin"] != null)
            {
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                // display product owner 
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name, t5.username_owner FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.id_owner = t5.id_owner WHERE t1.product_status = 'A'";
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
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdmin", "Login");
            }
        }
        public ActionResult Validate(FormCollection form)
        {
            SqlConnection myConnection = new SqlConnection();
            string id = form["id"];

            myConnection.ConnectionString = Mainconn;
            string Query3 = "UPDATE Product_owner SET product_status = 'A' WHERE Product_id = @Product_id";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@Product_id", id);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            return RedirectToAction("Dashboard");
        }
        public ActionResult Delete(FormCollection form)
        {
            SqlConnection myConnection = new SqlConnection();
            string noresi = form["noresi"];
            string id = form["id"];

            myConnection.ConnectionString = Mainconn;
            string Query3 = "UPDATE Estimasi_waktu SET no_resi = @no_resi WHERE cout_id = @cout_id";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@cout_id", id);
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