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
    public class UserController : Controller
    {
        //string Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        string Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        // GET: User
        public ActionResult Index()
        {
            if (Session["id_user"] != null)
            {
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = "select * from Toko_Profil";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.Toko_long = Convert.ToString(dr["Toko_long"]);
                        uc10.Toko_lat = Convert.ToString(dr["Toko_lat"]);
                        uc10.Toko_foto = Convert.ToString(dr["Toko_foto"]);
                        uc10.Toko_name = Convert.ToString(dr["Toko_name"]);
                        uc10.Toko_id = Convert.ToInt32(dr["Toko_id"]);

                        uc.Add(uc10);
                    }
                }
                // display product owner 
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.Berat, t1.product_harga, t1.Product_stock, t1.product_terjual, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.id_owner = t5.id_owner WHERE t1.product_status = 'A'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc2 = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        userclass uc10 = new userclass();
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
                        uc10.Product_id = Convert.ToInt32(dr["Product_id"]);
                        uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);
                        uc10.id_owner = Convert.ToInt32(dr["id_owner"]);
                        uc10.product_harga = Convert.ToInt32(dr["product_harga"]);
                        uc10.Product_stock = Convert.ToInt32(dr["Product_stock"]);
                        uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);
                        uc10.Toko_id = Convert.ToInt32(dr["Toko_id"]);
                        uc10.product_terjual = Convert.ToInt32(dr["product_terjual"]);
                        uc10.Berat = Convert.ToInt32(dr["Berat"]);

                        uc2.Add(uc10);
                    }
                }
                String sqlquery3 = "select * from KategoriProduct";
                SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
                SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
                DataTable ds3 = new DataTable();
                sda3.Fill(ds3);
                List<userclass> uc3 = new List<userclass>();
                {

                    foreach (DataRow dr in ds3.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);

                        uc3.Add(uc10);
                    }
                }
                String sqlquery4 = "select * from artist_Db";
                SqlCommand sqlcomm4 = new SqlCommand(sqlquery4, sqlconn);
                SqlDataAdapter sda4 = new SqlDataAdapter(sqlcomm4);
                DataTable ds4 = new DataTable();
                sda4.Fill(ds4);
                List<userclass> uc4 = new List<userclass>();
                {

                    foreach (DataRow dr in ds4.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);

                        uc4.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                ViewBag.uc3 = uc3;
                ViewBag.uc4 = uc4;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult DetailsProduct(int id)
        {
            // display product owner 
            SqlConnection sqlconn = new SqlConnection(Mainconn);
            String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.Berat, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID WHERE t1.Product_id = {id} AND t1.product_status = 'A'";
            SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
            DataTable ds2 = new DataTable();
            sda2.Fill(ds2);
            List<userclass> uc = new List<userclass>();
            {

                foreach (DataRow dr in ds2.Rows)
                {
                    userclass uc10 = new userclass();
                    uc10.RP = Convert.ToString(dr["RP"]);
                    uc10.Product_name = Convert.ToString(dr["Product_name"]);
                    uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                    uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                    uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                    uc10.product_status = Convert.ToString(dr["product_status"]);
                    uc10.address_owner = Convert.ToString(dr["address_owner"]);
                    uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                    uc10.artist_name = Convert.ToString(dr["artist_name"]);
                    uc10.Product_id = Convert.ToInt32(dr["Product_id"]);
                    uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);
                    uc10.id_owner = Convert.ToInt32(dr["id_owner"]);
                    uc10.product_harga = Convert.ToInt32(dr["product_harga"]);
                    uc10.Product_stock = Convert.ToInt32(dr["Product_stock"]);
                    uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);
                    uc10.Berat = Convert.ToInt32(dr["Berat"]);

                    uc.Add(uc10);
                }
            }
            //display image product
            String sqlquery = $"SELECT * from Image_product WHERE Product_id = {id}";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            List<userclass> uc2 = new List<userclass>();
            {

                foreach (DataRow dr in ds.Rows)
                {
                    userclass uc10 = new userclass();
                    uc10.img_name = Convert.ToString(dr["img_name"]);
                    uc10.Img_id = Convert.ToInt32(dr["Img_id"]);
                    uc10.Product_id = Convert.ToInt32(dr["Product_id"]);

                    uc2.Add(uc10);
                }
            }
            //display description product
            String sqlquery3 = $"SELECT * from Deskripsi_product WHERE Product_id = {id}";
            SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
            SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
            DataTable ds3 = new DataTable();
            sda3.Fill(ds3);
            List<userclass> uc3 = new List<userclass>();
            {

                foreach (DataRow dr in ds3.Rows)
                {
                    userclass uc10 = new userclass();
                    uc10.Deskripsi_isi = Convert.ToString(dr["Deskripsi_isi"]);
                    uc10.Deskripsi_details = Convert.ToString(dr["Deskripsi_details"]);
                    uc10.Deskripsi_kelebihan = Convert.ToString(dr["Deskripsi_kelebihan"]);

                    uc3.Add(uc10);
                }
            }
            ViewBag.uc = uc;
            ViewBag.uc2 = uc2;
            ViewBag.uc3 = uc3;
            sqlconn.Close();
            return View();
        }
        public ActionResult Detail(int Toko_id)
        {
            if (Session["id_user"] != null)
            {
                // display product owner 
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                // display product owner 
                //String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.Toko_id = t5.Toko_id WHERE t5.Toko_id={Toko_id} AND t1.product_status = 'A'";
                String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.id_owner = t5.id_owner WHERE t5.Toko_id={Toko_id} AND t1.product_status = 'A'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        userclass uc10 = new userclass();
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
                String sqlquery = $"SELECT t1.Toko_id, t1.Toko_name, t1.Toko_foto, t1.id_owner, t1.Toko_long, t1.Toko_lat, t1.Toko_nohp, t1.Toko_bank, Toko_norek, t1.Toko_status, t2.name_owner, t2.username_owner, t2.address_owner FROM Toko_Profil t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner WHERE t1.Toko_id = {Toko_id}";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc2 = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc11 = new userclass();
                        uc11.Toko_long = Convert.ToString(dr["Toko_long"]);
                        uc11.Toko_lat = Convert.ToString(dr["Toko_lat"]);
                        uc11.Toko_foto = Convert.ToString(dr["Toko_foto"]);
                        uc11.Toko_name = Convert.ToString(dr["Toko_name"]);
                        uc11.Toko_nohp = Convert.ToString(dr["Toko_nohp"]);
                        uc11.Toko_bank = Convert.ToString(dr["Toko_bank"]);
                        uc11.Toko_norek = Convert.ToString(dr["Toko_norek"]);
                        uc11.address_owner = Convert.ToString(dr["address_owner"]);
                        uc11.username_owner = Convert.ToString(dr["username_owner"]);
                        uc11.name_owner = Convert.ToString(dr["name_owner"]);
                        uc11.Toko_id = Convert.ToInt32(dr["Toko_id"]);
                        uc11.id_owner = Convert.ToInt32(dr["id_owner"]);

                        uc2.Add(uc11);
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
        public ActionResult Detailitem(int Kategori_ID)
        {
            if (Session["id_user"] != null)
            {
                // display product owner 
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                // display product owner 
                String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.id_owner = t5.id_owner WHERE t1.Kategori_ID={Kategori_ID} AND t1.product_status = 'A'";
                //String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.Toko_id = t5.Toko_id WHERE t1.Kategori_ID={Kategori_ID} AND t1.product_status = 'A'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        userclass uc10 = new userclass();
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
                String sqlquery3 = $"select * from KategoriProduct where Kategori_ID={Kategori_ID}";
                SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
                SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
                DataTable ds3 = new DataTable();
                sda3.Fill(ds3);
                List<userclass> uc2 = new List<userclass>();
                {

                    foreach (DataRow dr in ds3.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);

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
        public ActionResult Detailartis(int artist_ID)
        {
            if (Session["id_user"] != null)
            {
                // display product owner 
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                // display product owner 
                String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.id_owner = t5.id_owner WHERE t1.artist_ID={artist_ID} AND t1.product_status = 'A'";
                //String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Toko_id, t5.Toko_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Toko_Profil t5 ON t1.Toko_id = t5.Toko_id WHERE t1.artist_ID={artist_ID} AND t1.product_status = 'A'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        userclass uc10 = new userclass();
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
                String sqlquery4 = $"select * from artist_Db where artist_ID = {artist_ID}";
                SqlCommand sqlcomm4 = new SqlCommand(sqlquery4, sqlconn);
                SqlDataAdapter sda4 = new SqlDataAdapter(sqlcomm4);
                DataTable ds4 = new DataTable();
                sda4.Fill(ds4);
                List<userclass> uc4 = new List<userclass>();
                {

                    foreach (DataRow dr in ds4.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);

                        uc4.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc4 = uc4;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        //Add to cart product
        //public ActionResult AddToCart(FormCollection form, int Totalberat)
        //{
        //    SqlConnection myConnection = new SqlConnection();
        //    string SessionName = Session["id_user"].ToString();
        //    string kuantity = form["kuantity"];
        //    string productid = form["productid"];
        //    //string Totalberat = form["Totalberat"];

        //    //string checkQuery = "SELECT SUM(total_berat) FROM Cart_user WHERE id_user = '"+ SessionName + "' AND cart_status = 'CART'";

        //    myConnection.ConnectionString = Mainconn;
        //    string Query3 = "INSERT INTO Cart_user (Product_id, Cart_kuantity, id_user, cart_status, tanggal_pembelian, total_berat) " +
        //        "VALUES (@Product_id, @Cart_kuantity, @id_user, 'CART', CURRENT_TIMESTAMP, @total_berat)";
        //    using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
        //    {
        //        sqlmethod.Parameters.AddWithValue("@Product_id", productid);
        //        sqlmethod.Parameters.AddWithValue("@Cart_kuantity", kuantity);
        //        sqlmethod.Parameters.AddWithValue("@id_user", SessionName);
        //        sqlmethod.Parameters.AddWithValue("@total_berat", Totalberat);
        //        myConnection.Open();
        //        sqlmethod.ExecuteNonQuery();
        //        TempData["messsage"] = "success";
        //        myConnection.Close();
        //    }
        //    return RedirectToAction("Index");
        //} 
        //Add to cart product
        public ActionResult AddToCart(FormCollection form, int Totalberat)
        {
            int totalWeight = 0;
            using (SqlConnection myConnection = new SqlConnection(Mainconn))
            {
                string SessionName = Session["id_user"].ToString();
                string kuantity = form["kuantity"];
                string productid = form["productid"];
                //string Totalberat = form["Totalberat"];
                myConnection.Open();

                // hitung total berat dari semua item di cart
                string query = "SELECT SUM(total_berat) AS totalWeight FROM Cart_user WHERE id_user = '" + SessionName + "' AND cart_status = 'CART'";
                SqlCommand command = new SqlCommand(query, myConnection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["TotalWeight"] == DBNull.Value)
                    {

                    }
                    else
                    {
                        totalWeight = Convert.ToInt32(reader["TotalWeight"]);
                    }
                }

                reader.Close();

                // cek apakah total berat melebihi 10kg
                if (totalWeight + Totalberat > 10)
                {
                    ModelState.AddModelError("", "Total weight of cart items exceeds 10kg.");
                    return RedirectToAction("Validasicart");
                }
                else
                {

                    // tambahkan item ke cart
                    string Query3 = "INSERT INTO Cart_user (Product_id, Cart_kuantity, id_user, cart_status, tanggal_pembelian, total_berat) " +
                            "VALUES (@Product_id, @Cart_kuantity, @id_user, 'CART', CURRENT_TIMESTAMP, @total_berat)";
                    command = new SqlCommand(Query3, myConnection);
                    command.Parameters.AddWithValue("@Product_id", productid);
                    command.Parameters.AddWithValue("@Cart_kuantity", kuantity);
                    command.Parameters.AddWithValue("@id_user", SessionName);
                    command.Parameters.AddWithValue("@total_berat", Totalberat);
                    command.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Validasicart()
        {
            return View();
        }
        public ActionResult CartDetails()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "select * from Toko_Profil";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.Toko_long = Convert.ToString(dr["Toko_long"]);
                        uc10.Toko_lat = Convert.ToString(dr["Toko_lat"]);
                        uc10.Toko_foto = Convert.ToString(dr["Toko_foto"]);
                        uc10.Toko_name = Convert.ToString(dr["Toko_name"]);
                        uc10.Toko_id = Convert.ToInt32(dr["Toko_id"]);

                        uc.Add(uc10);
                    }
                }
                // display product owner 
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.Berat, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Cart_id, t5.Cart_kuantity, t5.total_harga, t5.cart_status, t5.id_user FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Cart_user t5 ON t1.Product_id = t5.Product_id WHERE t5.cart_status = 'CART' AND t5.id_user = '" + SessionName + "'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc2 = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.RP = Convert.ToString(dr["RP"]);
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.address_owner = Convert.ToString(dr["address_owner"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.Product_id = Convert.ToInt32(dr["Product_id"]);
                        uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);
                        uc10.id_owner = Convert.ToInt32(dr["id_owner"]);
                        uc10.product_harga = Convert.ToInt32(dr["product_harga"]);
                        uc10.Product_stock = Convert.ToInt32(dr["Product_stock"]);
                        uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);
                        uc10.Cart_id = Convert.ToInt32(dr["Cart_id"]);
                        uc10.id_user = Convert.ToInt32(dr["id_user"]);
                        uc10.Cart_kuantity = Convert.ToInt32(dr["Cart_kuantity"]);
                        uc10.Berat = Convert.ToInt32(dr["Berat"]);
                        //uc10.total_harga = Convert.ToInt32(dr["total_harga"]);
                        uc10.cart_status = Convert.ToString(dr["cart_status"]);

                        uc2.Add(uc10);
                    }
                }
                string sqlquery3 = "SELECT COUNT(Cart_id) AS VALIDASI FROM Cart_user WHERE id_user = '" + SessionName + "' AND cart_status = 'CART'";
                SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
                SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
                DataTable ds3 = new DataTable();
                sda3.Fill(ds3);
                List<userclass> uc3 = new List<userclass>();
                {

                    foreach (DataRow dr in ds3.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.VALIDASI = Convert.ToString(dr["VALIDASI"]);

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
                return RedirectToAction("LoginUser", "Login");
            }
        }
        //public ActionResult EditCart(FormCollection form, List<string> cartid, List<string> quantity, List<string> subtotal, List<string> jumlahberat)
        //{
        //    if (Session["id_user"] != null)
        //    {
        //        List<userclass> jc = new List<userclass>();
        //        //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        //        var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
        //        SqlConnection myConnection = new SqlConnection();
        //        myConnection.ConnectionString = connectionString;
        //        myConnection.Open();
        //        SqlConnection sqlconn = new SqlConnection(connectionString);
        //        string varString = "";
        //        for (int i = 0; i < cartid.Count(); i++)
        //        {
        //            varString += $"UPDATE Cart_user set total_harga = @total_harga{i} ,Cart_kuantity = @Cart_kuantity{i}, total_berat = @total_berat{i}  where Cart_id = @Cart_id{i};";
        //        }
        //        SqlCommand sqlcommm = new SqlCommand(varString, sqlconn);
        //        sqlconn.Open();
        //        {
        //            for (int i = 0; i < cartid.Count(); i++)
        //            {
        //                sqlcommm.Parameters.AddWithValue("@total_harga" + i, subtotal[i]);
        //                sqlcommm.Parameters.AddWithValue("@Cart_kuantity" + i, quantity[i]);
        //                sqlcommm.Parameters.AddWithValue("@Cart_id" + i, cartid[i]);
        //                sqlcommm.Parameters.AddWithValue("@total_berat" + i, jumlahberat[i]);
        //            }

        //            sqlcommm.ExecuteNonQuery();
        //            sqlconn.Close();
        //        }
        //        myConnection.Close();
        //        return Redirect("CartDetails");
        //    }
        //    else
        //    {
        //        return RedirectToAction("LoginUser", "Login");
        //    }
        //} 
        public ActionResult EditCart(FormCollection form, List<string> cartid, List<string> quantity, List<string> subtotal, List<string> jumlahberat)
        {
            if (Session["id_user"] != null)
            {
                int totalWeight = 0;
                using (SqlConnection myConnection = new SqlConnection(Mainconn))
                {
                    string SessionName = Session["id_user"].ToString();
                    string kuantity = form["kuantity"];
                    string productid = form["productid"];
                    //string Totalberat = form["Totalberat"];
                    myConnection.Open();
                    SqlConnection sqlconn = new SqlConnection(Mainconn);
                    // hitung total berat dari semua item di cart
                    string query = "SELECT SUM(total_berat) AS totalWeight FROM Cart_user WHERE id_user = '" + SessionName + "' AND cart_status = 'CART'";
                    SqlCommand command = new SqlCommand(query, myConnection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader["TotalWeight"] == DBNull.Value)
                        {

                        }
                        else
                        {
                            totalWeight = Convert.ToInt32(reader["TotalWeight"]);
                        }
                    }

                    reader.Close();
                    int x = 0;
                    foreach(string checkweight in jumlahberat)
                    {
                        x += int.Parse(checkweight);
                    }
                    // cek apakah total berat melebihi 10kg
                    if (x > 10)
                    {
                        ModelState.AddModelError("", "Total weight of cart items exceeds 10kg.");
                        return RedirectToAction("Validasicart");
                    }
                    //else 
                    //{
                    string varString = "";
                    for (int i = 0; i < cartid.Count(); i++)
                    {
                        varString += $"UPDATE Cart_user set total_harga = @total_harga{i} ,Cart_kuantity = @Cart_kuantity{i}, total_berat = @total_berat{i}  where Cart_id = @Cart_id{i};";
                    }
                    SqlCommand sqlcommm = new SqlCommand(varString, sqlconn);
                    sqlconn.Open();
                    {
                        for (int i = 0; i < cartid.Count(); i++)
                        {
                            sqlcommm.Parameters.AddWithValue("@total_harga" + i, subtotal[i]);
                            sqlcommm.Parameters.AddWithValue("@Cart_kuantity" + i, quantity[i]);
                            sqlcommm.Parameters.AddWithValue("@Cart_id" + i, cartid[i]);
                            sqlcommm.Parameters.AddWithValue("@total_berat" + i, jumlahberat[i]);
                        }

                        sqlcommm.ExecuteNonQuery();
                        sqlconn.Close();
                        //}
                    }
                }
                return Redirect("CartDetails");
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        // for the checkout page
        public ActionResult Checkout()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "select * from Toko_Profil";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.Toko_long = Convert.ToString(dr["Toko_long"]);
                        uc10.Toko_lat = Convert.ToString(dr["Toko_lat"]);
                        uc10.Toko_foto = Convert.ToString(dr["Toko_foto"]);
                        uc10.Toko_name = Convert.ToString(dr["Toko_name"]);
                        uc10.Toko_id = Convert.ToInt32(dr["Toko_id"]);

                        uc.Add(uc10);
                    }
                }
                // display product owner 
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Cart_id, t5.Cart_kuantity, t5.total_harga, t5.cart_status, t5.id_user FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Cart_user t5 ON t1.Product_id = t5.Product_id WHERE t5.cart_status = 'CART' AND t5.id_user = '" + SessionName + "'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc2 = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.RP = Convert.ToString(dr["RP"]);
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.address_owner = Convert.ToString(dr["address_owner"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.Product_id = Convert.ToInt32(dr["Product_id"]);
                        uc10.Kategori_ID = Convert.ToInt32(dr["Kategori_ID"]);
                        uc10.id_owner = Convert.ToInt32(dr["id_owner"]);
                        uc10.product_harga = Convert.ToInt32(dr["product_harga"]);
                        uc10.Product_stock = Convert.ToInt32(dr["Product_stock"]);
                        uc10.artist_ID = Convert.ToInt32(dr["artist_ID"]);
                        uc10.Cart_id = Convert.ToInt32(dr["Cart_id"]);
                        uc10.id_user = Convert.ToInt32(dr["id_user"]);
                        uc10.Cart_kuantity = Convert.ToInt32(dr["Cart_kuantity"]);
                        //uc10.total_harga = Convert.ToInt32(dr["total_harga"]);
                        uc10.cart_status = Convert.ToString(dr["cart_status"]);

                        uc2.Add(uc10);
                    }
                }
                String sqlquery3 = " SELECT COUNT(Cart_id) AS VALIDASI FROM Cart_user WHERE id_user = '" + SessionName + "' AND cart_status = 'CART'";
                SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
                SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
                DataTable ds3 = new DataTable();
                sda3.Fill(ds3);
                List<userclass> uc3 = new List<userclass>();
                {

                    foreach (DataRow dr in ds3.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.VALIDASI = Convert.ToString(dr["VALIDASI"]);

                        uc3.Add(uc10);
                    }
                }
                String sqlquery4 = "SELECT TOP (1) * from alamat_user WHERE id_user = '" + SessionName + "'";
                SqlCommand sqlcomm4 = new SqlCommand(sqlquery4, sqlconn);
                SqlDataAdapter sda4 = new SqlDataAdapter(sqlcomm4);
                DataTable ds4 = new DataTable();
                sda4.Fill(ds4);
                List<userclass> uc4 = new List<userclass>();
                {

                    foreach (DataRow dr in ds4.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.alamt_name = Convert.ToString(dr["alamt_name"]);
                        uc10.lattitude_user = Convert.ToString(dr["lattitude_user"]);
                        uc10.longitude_user = Convert.ToString(dr["longitude_user"]);
                        uc10.name_buyer = Convert.ToString(dr["name_buyer"]);
                        uc10.almt_Id = Convert.ToInt32(dr["almt_Id"]);
                        uc10.zip_code = Convert.ToInt32(dr["zip_code"]);
                        uc10.prov_id = Convert.ToInt32(dr["prov_id"]);
                        uc10.kab_id = Convert.ToInt32(dr["kab_id"]);
                        uc10.country_id = Convert.ToInt32(dr["country_id"]);
                        uc10.no_hpbuy = Convert.ToString(dr["no_hpbuy"]);
                        uc10.id_user = Convert.ToInt32(dr["id_user"]);

                        uc4.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                ViewBag.uc3 = uc3;
                ViewBag.uc4 = uc4;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult cartpaylogic(FormCollection form, List<string> subtotal, List<string> cartid, List<string> productid)
        {
            if (Session["id_user"] != null)
            {
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(Mainconn);
                //myConnection.ConnectionString = Mainconn;
                myConnection.Open();
                string SessionName = Session["id_user"].ToString();
                string totalharga = form["totalharga"];
                string alamat = form["alamat"];
                string payment = form["payment"];
                string shipping = form["shipping"];
                SqlConnection sqlconn2 = new SqlConnection(Mainconn);
                string Query3 = "INSERT INTO checkout_user (all_total, pay_ID, eks_id,  id_user, id_owner, almt_Id, cout_status, created_at, updated_at) VALUES (@all_total, @pay_ID, @eks_id, @id_user, '1', @almt_Id, 'CHECKOUT', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, sqlconn2))
                {
                    sqlmethod.Parameters.AddWithValue("@all_total", totalharga);
                    sqlmethod.Parameters.AddWithValue("@pay_ID", payment);
                    sqlmethod.Parameters.AddWithValue("@eks_id", shipping);
                    sqlmethod.Parameters.AddWithValue("@id_user", SessionName);
                    sqlmethod.Parameters.AddWithValue("@almt_Id", alamat);
                    sqlconn2.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    sqlconn2.Close();
                }
                int Method = 0;
                string method = "SELECT * FROM checkout_user";
                SqlDataAdapter damethod = new SqlDataAdapter(method, myConnection);
                DataTable dtmethod = new DataTable();
                damethod.Fill(dtmethod);
                foreach (DataRow dr in dtmethod.Rows)
                {
                    Method = (int)dr["cout_id"];
                }

                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string varString = "";
                for (int i = 0; i < cartid.Count(); i++)
                {
                    varString += $"UPDATE Cart_user SET cout_id = @cout_id,cart_status = 'CHECKOUT', tanggal_checkout = CURRENT_TIMESTAMP WHERE Cart_id = @Cart_id{i};";
                }
                SqlCommand sqlcommm = new SqlCommand(varString, sqlconn);
                sqlconn.Open();
                {
                    for (int i = 0; i < cartid.Count(); i++)
                    {
                        sqlcommm.Parameters.AddWithValue("@Cart_id" + i, cartid[i]);
                    }
                    sqlcommm.Parameters.AddWithValue("@cout_id", Method);
                    sqlcommm.ExecuteNonQuery();
                    sqlconn.Close();
                }
                SqlConnection sqlconn3 = new SqlConnection(Mainconn);
                string varString3 = "";
                for (int i = 0; i < cartid.Count(); i++)
                {
                    varString3 += $"UPDATE Product_owner SET Product_stock = Product_stock - (SELECT (Cart_kuantity) FROM Cart_user where Cart_id = @Cart_id{i}), product_terjual = product_terjual + (SELECT (Cart_kuantity) FROM Cart_user where Cart_id = @Cart_id{i}) WHERE Product_id = @Product_id{i};";
                }
                SqlCommand sqlcommm3 = new SqlCommand(varString3, sqlconn3);
                sqlconn3.Open();
                {
                    for (int i = 0; i < cartid.Count(); i++)
                    {
                        sqlcommm3.Parameters.AddWithValue("@Cart_id" + i, cartid[i]);
                        sqlcommm3.Parameters.AddWithValue("@Product_id" + i, productid[i]);
                    }
                    sqlcommm3.ExecuteNonQuery();
                    sqlconn3.Close();
                }

                myConnection.Close();
                return RedirectToAction("ConfirmOrderUser");
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult AddressUser()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "SELECT * from alamat_user WHERE id_user = '" + SessionName + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.alamt_name = Convert.ToString(dr["alamt_name"]);
                        uc10.lattitude_user = Convert.ToString(dr["lattitude_user"]);
                        uc10.longitude_user = Convert.ToString(dr["longitude_user"]);
                        uc10.name_buyer = Convert.ToString(dr["name_buyer"]);
                        uc10.almt_Id = Convert.ToInt32(dr["almt_Id"]);
                        uc10.zip_code = Convert.ToInt32(dr["zip_code"]);
                        uc10.prov_id = Convert.ToInt32(dr["prov_id"]);
                        uc10.kab_id = Convert.ToInt32(dr["kab_id"]);
                        uc10.country_id = Convert.ToInt32(dr["country_id"]);
                        uc10.no_hpbuy = Convert.ToString(dr["no_hpbuy"]);
                        uc10.id_user = Convert.ToInt32(dr["id_user"]);

                        uc.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult AddNewAddress(FormCollection form)
        {
            if (Session["id_user"] != null)
            {
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(Mainconn);
                //myConnection.ConnectionString = connectionString;
                myConnection.Open();
                string SessionName = Session["id_user"].ToString();
                string firstName = form["firstName"];
                string nohp = form["nohp"];
                string address = form["address"];
                string lattitude = form["lattitude"];
                string longitude = form["longitude"];
                string country = form["country"];
                string prov = form["prov"];
                string kab = form["kab"];
                string zip = form["zip"];
                SqlConnection sqlconn2 = new SqlConnection(Mainconn);
                string Query3 = "INSERT INTO alamat_user (alamt_name, zip_code, prov_id, country_id, kab_id, lattitude_user, longitude_user, name_buyer, no_hpbuy, id_user) VALUES (@alamt_name, @zip_code, @prov_id, @country_id, @kab_id, @lattitude_user, @longitude_user, @name_buyer, @no_hpbuy, @id_user)";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, sqlconn2))
                {
                    sqlmethod.Parameters.AddWithValue("@alamt_name", address);
                    sqlmethod.Parameters.AddWithValue("@zip_code", zip);
                    sqlmethod.Parameters.AddWithValue("@prov_id", prov);
                    sqlmethod.Parameters.AddWithValue("@country_id", country);
                    sqlmethod.Parameters.AddWithValue("@lattitude_user", lattitude);
                    sqlmethod.Parameters.AddWithValue("@longitude_user", longitude);
                    sqlmethod.Parameters.AddWithValue("@name_buyer", firstName);
                    sqlmethod.Parameters.AddWithValue("@no_hpbuy", nohp);
                    sqlmethod.Parameters.AddWithValue("@id_user", SessionName);
                    sqlmethod.Parameters.AddWithValue("@kab_id", kab);
                    sqlconn2.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    sqlconn2.Close();
                }
                return RedirectToAction("AddressUser");
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult ConfirmOrderUser()
        {
            if (Session["id_user"] != null)
            {
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                int Method = 0;
                string method = "SELECT * FROM checkout_user";
                SqlDataAdapter damethod = new SqlDataAdapter(method, sqlconn);
                DataTable dtmethod = new DataTable();
                damethod.Fill(dtmethod);
                foreach (DataRow dr in dtmethod.Rows)
                {
                    Method = (int)dr["cout_id"];
                }

                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                String sqlquery = "SELECT t1.cout_id, t1.all_total, t1.almt_Id, t1.pay_ID, t1.eks_id, t2.lattitude_user, t2.longitude_user, t3.eks_harga FROM checkout_user t1 JOIN alamat_user t2 ON t1.almt_Id = t2.almt_Id JOIN Ekspedis_c t3 ON t1.eks_id = t3.eks_id WHERE t1.cout_id = '" + Method + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.lattitude_user = Convert.ToString(dr["lattitude_user"]);
                        uc10.longitude_user = Convert.ToString(dr["longitude_user"]);
                        uc10.cout_id = Convert.ToInt32(dr["cout_id"]);
                        uc10.all_total = Convert.ToInt32(dr["all_total"]);
                        uc10.almt_Id = Convert.ToInt32(dr["almt_Id"]);
                        uc10.eks_id = Convert.ToInt32(dr["eks_id"]);
                        uc10.eks_harga = Convert.ToInt32(dr["eks_harga"]);
                        uc10.pay_ID = Convert.ToInt32(dr["pay_ID"]);

                        uc.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult ConfirmOrderUser2(int id)
        {
            if (Session["id_user"] != null)
            {
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                int Method = 0;
                string method = "SELECT * FROM checkout_user";
                SqlDataAdapter damethod = new SqlDataAdapter(method, sqlconn);
                DataTable dtmethod = new DataTable();
                damethod.Fill(dtmethod);
                foreach (DataRow dr in dtmethod.Rows)
                {
                    Method = (int)dr["cout_id"];
                }

                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                String sqlquery = $"SELECT t1.cout_id, t1.all_total, t1.almt_Id, t1.pay_ID, t1.eks_id, t2.lattitude_user, t2.longitude_user, t3.eks_harga FROM checkout_user t1 JOIN alamat_user t2 ON t1.almt_Id = t2.almt_Id JOIN Ekspedis_c t3 ON t1.eks_id = t3.eks_id WHERE t1.cout_id = {id}";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.lattitude_user = Convert.ToString(dr["lattitude_user"]);
                        uc10.longitude_user = Convert.ToString(dr["longitude_user"]);
                        uc10.cout_id = Convert.ToInt32(dr["cout_id"]);
                        uc10.all_total = Convert.ToInt32(dr["all_total"]);
                        uc10.almt_Id = Convert.ToInt32(dr["almt_Id"]);
                        uc10.eks_id = Convert.ToInt32(dr["eks_id"]);
                        uc10.eks_harga = Convert.ToInt32(dr["eks_harga"]);
                        uc10.pay_ID = Convert.ToInt32(dr["pay_ID"]);

                        uc.Add(uc10);
                    }
                }
                ViewBag.uc = uc;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult statusorder(int id)
        {
            if (Session["id_user"] != null)
            {
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                int Method = 0;
                string method = "SELECT * FROM checkout_user";
                SqlDataAdapter damethod = new SqlDataAdapter(method, sqlconn);
                DataTable dtmethod = new DataTable();
                damethod.Fill(dtmethod);
                foreach (DataRow dr in dtmethod.Rows)
                {
                    Method = (int)dr["cout_id"];
                }

                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                String sqlquery = $"SELECT t1.estimasi_ID, t1.cout_id, t1.estimasi_sampai, t1.no_resi, t1.status, t1.waktu_pengiriman, t2.all_total, t2.created_at, t2.updated_at FROM Estimasi_waktu t1 JOIN checkout_user t2 ON t1.cout_id = t2.cout_id WHERE t1.cout_id = {id}";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                {

                    foreach (DataRow dr in ds.Rows)
                    {
                        userclass uc10 = new userclass();
                        uc10.estimasi_sampai = Convert.ToString(dr["estimasi_sampai"]);
                        uc10.no_resi = Convert.ToString(dr["no_resi"]);
                        uc10.status = Convert.ToString(dr["status"]);
                        uc10.waktu_pengiriman = Convert.ToDateTime(dr["waktu_pengiriman"]);
                        uc10.created_at = Convert.ToDateTime(dr["created_at"]);
                        uc10.updated_at = Convert.ToDateTime(dr["updated_at"]);
                        uc10.cout_id = Convert.ToInt32(dr["cout_id"]);
                        uc10.all_total = Convert.ToInt32(dr["all_total"]);
                        uc10.estimasi_ID = Convert.ToInt32(dr["estimasi_ID"]);

                        uc.Add(uc10);
                    }
                }
                string sqlquery2 = $"SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1 FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id WHERE t1.cout_id = {id}";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc2 = new List<userclass>();
                foreach (DataRow dr1 in ds2.Rows)
                {
                    userclass data2 = new userclass();
                    //data2.swr_toy = (string)dr1["swr_toy"];
                    //data2.swr_desc = (string)dr1["swr_desc"];
                    //data2.swr_rem = (string)dr1["swr_rem"];
                    //data2.created_at = (DateTime)dr1["created_at"];
                    //data2.update_at = (DateTime)dr1["update_at"];
                    //data2.swr_purN = (string)dr1["swr_purN"];
                    data2.product_img1 = (string)dr1["product_img1"];
                    data2.Product_name = (string)dr1["Product_name"];
                    data2.Cart_id = Convert.ToInt32(dr1["Cart_id"]);
                    data2.Product_id = Convert.ToInt32(dr1["Product_id"]);
                    data2.Cart_kuantity = Convert.ToInt32(dr1["Cart_kuantity"]);
                    data2.total_harga = Convert.ToInt32(dr1["total_harga"]);
                    data2.id_user = Convert.ToInt32(dr1["id_user"]);
                    data2.cout_id = Convert.ToInt32(dr1["cout_id"]);
                    uc2.Add(data2);
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult cancelorder(FormCollection form)
        {
            if (Session["id_user"] != null)
            {
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(Mainconn);
                //myConnection.ConnectionString = connectionString;
                myConnection.Open();
                string SessionName = Session["id_user"].ToString();
                string itemid = form["itemid"];
                SqlConnection sqlconn2 = new SqlConnection(Mainconn);
                string Query3 = "DELETE FROM Cart_user WHERE cout_id = '" + itemid + "'";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, sqlconn2))
                {
                    sqlconn2.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    sqlconn2.Close();
                }
                string Query4 = "DELETE FROM checkout_user WHERE cout_id = '" + itemid + "'";
                using (SqlCommand sqlmethod = new SqlCommand(Query4, sqlconn2))
                {
                    sqlconn2.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    sqlconn2.Close();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult confirmorder(FormCollection form, HttpPostedFileBase buktipembayaran)
        {
            if (Session["id_user"] != null)
            {
                string sewnPatternImage = "BuktiPembayaran" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(buktipembayaran.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern = Path.Combine(Server.MapPath("/gambar/BuktiPembayaran/"), sewnPatternImage);
                buktipembayaran.SaveAs(filePathsewnPattern);

                //var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
                //var connectionString = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(Mainconn);
                //myConnection.ConnectionString = connectionString;
                myConnection.Open();
                string SessionName = Session["id_user"].ToString();
                string itemid = form["itemid"];
                SqlConnection sqlconn2 = new SqlConnection(Mainconn);
                string Query3 = "UPDATE Cart_user SET cart_status = 'PAYMENT' WHERE cout_id = '" + itemid + "'";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, sqlconn2))
                {
                    sqlconn2.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    sqlconn2.Close();
                }
                string Query4 = "UPDATE checkout_user SET cout_status = 'PAYMENT', payment_history='" + sewnPatternImage + "', updated_at = CURRENT_TIMESTAMP WHERE cout_id = '" + itemid + "'";
                using (SqlCommand sqlmethod = new SqlCommand(Query4, sqlconn2))
                {
                    sqlconn2.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    sqlconn2.Close();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult HistoryOrder()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = $"SELECT * FROM checkout_user WHERE cout_status = 'PAYMENT' AND id_user = '" + SessionName + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                List<userclass> uc2 = new List<userclass>();
                foreach (DataRow dr in ds.Rows)
                {
                    userclass data = new userclass();
                    data.cout_id = (int)dr["cout_id"];
                    data.updated_at = (DateTime)dr["updated_at"];
                    uc.Add(data);
                    string sqlquery2 = $"SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1 FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id WHERE t1.cout_id = '{data.cout_id}' AND t1.id_user = '" + SessionName + "'";
                    SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                    DataTable ds2 = new DataTable();
                    sda2.Fill(ds2);
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        userclass data2 = new userclass();
                        //data2.swr_toy = (string)dr1["swr_toy"];
                        //data2.swr_desc = (string)dr1["swr_desc"];
                        //data2.swr_rem = (string)dr1["swr_rem"];
                        //data2.created_at = (DateTime)dr1["created_at"];
                        //data2.update_at = (DateTime)dr1["update_at"];
                        //data2.swr_purN = (string)dr1["swr_purN"];
                        data2.product_img1 = (string)dr1["product_img1"];
                        data2.Product_name = (string)dr1["Product_name"];
                        data2.Cart_id = Convert.ToInt32(dr1["Cart_id"]);
                        data2.Product_id = Convert.ToInt32(dr1["Product_id"]);
                        data2.Cart_kuantity = Convert.ToInt32(dr1["Cart_kuantity"]);
                        data2.total_harga = Convert.ToInt32(dr1["total_harga"]);
                        data2.id_user = Convert.ToInt32(dr1["id_user"]);
                        data2.cout_id = Convert.ToInt32(dr1["cout_id"]);
                        uc2.Add(data2);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult HistoryOrderCheckout()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = $"SELECT * FROM checkout_user WHERE cout_status = 'CHECKOUT' AND id_user = '" + SessionName + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                List<userclass> uc2 = new List<userclass>();
                foreach (DataRow dr in ds.Rows)
                {
                    userclass data = new userclass();
                    data.cout_id = (int)dr["cout_id"];
                    data.updated_at = (DateTime)dr["updated_at"];
                    uc.Add(data);
                    string sqlquery2 = $"SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1 FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id WHERE t1.cout_id = '{data.cout_id}' AND t1.id_user = '" + SessionName + "'";
                    SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                    DataTable ds2 = new DataTable();
                    sda2.Fill(ds2);
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        userclass data2 = new userclass();
                        //data2.swr_toy = (string)dr1["swr_toy"];
                        //data2.swr_desc = (string)dr1["swr_desc"];
                        //data2.swr_rem = (string)dr1["swr_rem"];
                        //data2.created_at = (DateTime)dr1["created_at"];
                        //data2.update_at = (DateTime)dr1["update_at"];
                        //data2.swr_purN = (string)dr1["swr_purN"];
                        data2.product_img1 = (string)dr1["product_img1"];
                        data2.Product_name = (string)dr1["Product_name"];
                        data2.Cart_id = Convert.ToInt32(dr1["Cart_id"]);
                        data2.Product_id = Convert.ToInt32(dr1["Product_id"]);
                        data2.Cart_kuantity = Convert.ToInt32(dr1["Cart_kuantity"]);
                        data2.total_harga = Convert.ToInt32(dr1["total_harga"]);
                        data2.id_user = Convert.ToInt32(dr1["id_user"]);
                        data2.cout_id = Convert.ToInt32(dr1["cout_id"]);
                        uc2.Add(data2);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult HistoryOrderPacked()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = $"SELECT * FROM checkout_user WHERE cout_status = 'DIKEMAS' AND id_user = '" + SessionName + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                List<userclass> uc2 = new List<userclass>();
                foreach (DataRow dr in ds.Rows)
                {
                    userclass data = new userclass();
                    data.cout_id = (int)dr["cout_id"];
                    data.updated_at = (DateTime)dr["updated_at"];
                    uc.Add(data);
                    string sqlquery2 = $"SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1 FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id WHERE t1.cout_id = '{data.cout_id}' AND t1.id_user = '" + SessionName + "'";
                    SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                    DataTable ds2 = new DataTable();
                    sda2.Fill(ds2);
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        userclass data2 = new userclass();
                        //data2.swr_toy = (string)dr1["swr_toy"];
                        //data2.swr_desc = (string)dr1["swr_desc"];
                        //data2.swr_rem = (string)dr1["swr_rem"];
                        //data2.created_at = (DateTime)dr1["created_at"];
                        //data2.update_at = (DateTime)dr1["update_at"];
                        //data2.swr_purN = (string)dr1["swr_purN"];
                        data2.product_img1 = (string)dr1["product_img1"];
                        data2.Product_name = (string)dr1["Product_name"];
                        data2.Cart_id = Convert.ToInt32(dr1["Cart_id"]);
                        data2.Product_id = Convert.ToInt32(dr1["Product_id"]);
                        data2.Cart_kuantity = Convert.ToInt32(dr1["Cart_kuantity"]);
                        data2.total_harga = Convert.ToInt32(dr1["total_harga"]);
                        data2.id_user = Convert.ToInt32(dr1["id_user"]);
                        data2.cout_id = Convert.ToInt32(dr1["cout_id"]);
                        uc2.Add(data2);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        } 
        public ActionResult HistoryOrderSent()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = $"SELECT * FROM checkout_user WHERE cout_status = 'DIKIRIM' AND id_user = '" + SessionName + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                List<userclass> uc2 = new List<userclass>();
                foreach (DataRow dr in ds.Rows)
                {
                    userclass data = new userclass();
                    data.cout_id = (int)dr["cout_id"];
                    data.updated_at = (DateTime)dr["updated_at"];
                    uc.Add(data);
                    string sqlquery2 = $"SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1 FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id WHERE t1.cout_id = '{data.cout_id}' AND t1.id_user = '" + SessionName + "'";
                    SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                    DataTable ds2 = new DataTable();
                    sda2.Fill(ds2);
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        userclass data2 = new userclass();
                        //data2.swr_toy = (string)dr1["swr_toy"];
                        //data2.swr_desc = (string)dr1["swr_desc"];
                        //data2.swr_rem = (string)dr1["swr_rem"];
                        //data2.created_at = (DateTime)dr1["created_at"];
                        //data2.update_at = (DateTime)dr1["update_at"];
                        //data2.swr_purN = (string)dr1["swr_purN"];
                        data2.product_img1 = (string)dr1["product_img1"];
                        data2.Product_name = (string)dr1["Product_name"];
                        data2.Cart_id = Convert.ToInt32(dr1["Cart_id"]);
                        data2.Product_id = Convert.ToInt32(dr1["Product_id"]);
                        data2.Cart_kuantity = Convert.ToInt32(dr1["Cart_kuantity"]);
                        data2.total_harga = Convert.ToInt32(dr1["total_harga"]);
                        data2.id_user = Convert.ToInt32(dr1["id_user"]);
                        data2.cout_id = Convert.ToInt32(dr1["cout_id"]);
                        uc2.Add(data2);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult HistoryOrderFinish()
        {
            if (Session["id_user"] != null)
            {
                string SessionName = Session["id_user"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = $"SELECT * FROM checkout_user WHERE cout_status = 'FINISH' AND id_user = '" + SessionName + "'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<userclass> uc = new List<userclass>();
                List<userclass> uc2 = new List<userclass>();
                foreach (DataRow dr in ds.Rows)
                {
                    userclass data = new userclass();
                    data.cout_id = (int)dr["cout_id"];
                    data.updated_at = (DateTime)dr["updated_at"];
                    uc.Add(data);
                    string sqlquery2 = $"SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1 FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id WHERE t1.cout_id = '{data.cout_id}' AND t1.id_user = '" + SessionName + "'";
                    SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                    DataTable ds2 = new DataTable();
                    sda2.Fill(ds2);
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        userclass data2 = new userclass();
                        //data2.swr_toy = (string)dr1["swr_toy"];
                        //data2.swr_desc = (string)dr1["swr_desc"];
                        //data2.swr_rem = (string)dr1["swr_rem"];
                        //data2.created_at = (DateTime)dr1["created_at"];
                        //data2.update_at = (DateTime)dr1["update_at"];
                        //data2.swr_purN = (string)dr1["swr_purN"];
                        data2.product_img1 = (string)dr1["product_img1"];
                        data2.Product_name = (string)dr1["Product_name"];
                        data2.Cart_id = Convert.ToInt32(dr1["Cart_id"]);
                        data2.Product_id = Convert.ToInt32(dr1["Product_id"]);
                        data2.Cart_kuantity = Convert.ToInt32(dr1["Cart_kuantity"]);
                        data2.total_harga = Convert.ToInt32(dr1["total_harga"]);
                        data2.id_user = Convert.ToInt32(dr1["id_user"]);
                        data2.cout_id = Convert.ToInt32(dr1["cout_id"]);
                        uc2.Add(data2);
                    }
                }
                ViewBag.uc = uc;
                ViewBag.uc2 = uc2;
                sqlconn.Close();
                return View();
            }
            else
            {
                return RedirectToAction("LoginUser", "Login");
            }
        }
    }
}