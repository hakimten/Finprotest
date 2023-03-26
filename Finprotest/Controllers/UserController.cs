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
    public class UserController : Controller
    {
        String Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        // GET: User
        public ActionResult Index()
        {
            if (Session["id_user"] != null)
            {
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
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID WHERE t1.product_status = 'A'";
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
                return RedirectToAction("LoginUser", "Login");
            }
        }
        public ActionResult DetailsProduct(int id)
        {
            // display product owner 
            SqlConnection sqlconn = new SqlConnection(Mainconn);
            String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID WHERE t1.Product_id = {id} AND t1.product_status = 'A'";
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

                    uc.Add(uc10);
                }
            }
            //menampilkan lattitude & longitude Toko
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
            ViewBag.uc = uc;
            ViewBag.uc2 = uc2;
            sqlconn.Close();
            return View();
        }
        //Add to cart product
        public ActionResult AddToCart(FormCollection form)
        {
            SqlConnection myConnection = new SqlConnection();
            string SessionName = Session["id_user"].ToString();
            string kuantity = form["kuantity"];
            string productid = form["productid"];

            myConnection.ConnectionString = Mainconn;
            string Query3 = "INSERT INTO Cart_user (Product_id, Cart_kuantity, id_user, cart_status, tanggal_pembelian) VALUES (@Product_id, @Cart_kuantity, @id_user, 'CART', CURRENT_TIMESTAMP)";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@Product_id", productid);
                sqlmethod.Parameters.AddWithValue("@Cart_kuantity", kuantity);
                sqlmethod.Parameters.AddWithValue("@id_user", SessionName);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            return RedirectToAction("Index");
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
                String sqlquery2 = "SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name, t5.Cart_id, t5.Cart_kuantity, t5.total_harga, t5.cart_status, t5.id_user FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Cart_user t5 ON t1.Product_id = t5.Product_id WHERE t5.cart_status = 'CART' AND t5.id_user = '"+ SessionName +"'";
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
                String sqlquery3 = " SELECT COUNT(Cart_id) AS VALIDASI FROM Cart_user WHERE id_user = '"+ SessionName +"' AND cart_status = 'CART'";
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
        public ActionResult EditCart(FormCollection form, List<string> cartid, List<string> quantity, List<string> subtotal)
        {
            if (Session["id_user"] != null)
            {
                List<userclass> jc = new List<userclass>();
                var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = connectionString;
                myConnection.Open();


                SqlConnection sqlconn = new SqlConnection(connectionString);
                string varString = "";
                for (int i = 0; i < cartid.Count(); i++)
                {
                    varString += $"UPDATE Cart_user set total_harga = @total_harga{i} ,Cart_kuantity = @Cart_kuantity{i} where Cart_id = @Cart_id{i};";
                }
                SqlCommand sqlcommm = new SqlCommand(varString, sqlconn);
                sqlconn.Open();
                {
                    for (int i = 0; i < cartid.Count(); i++)
                    {
                        sqlcommm.Parameters.AddWithValue("@total_harga" + i, subtotal[i]);
                        sqlcommm.Parameters.AddWithValue("@Cart_kuantity" + i, quantity[i]);
                        sqlcommm.Parameters.AddWithValue("@Cart_id" + i, cartid[i]);
                    }

                    sqlcommm.ExecuteNonQuery();
                    sqlconn.Close();
                }
                myConnection.Close();
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
                var connectionString = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = connectionString;
                myConnection.Open();
                string SessionName = Session["id_user"].ToString();
                string totalharga = form["totalharga"];
                string alamat = form["alamat"];
                string payment = form["payment"];
                string shipping = form["shipping"];
                SqlConnection sqlconn2 = new SqlConnection(Mainconn);
                string Query3 = "INSERT INTO checkout_user (all_total, pay_ID, eks_id,  id_user, almt_Id, cout_status, created_at, updated_at) VALUES (@all_total, @pay_ID, @eks_id, @id_user, @almt_Id, 'CHECKOUT', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";
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
                    varString3 += $"UPDATE Product_owner SET Product_stock = Product_stock - (SELECT (Cart_kuantity) FROM Cart_user where Cart_id = @Cart_id{i}) WHERE Product_id = @Product_id{i};";
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
                String sqlquery = "SELECT * from alamat_user WHERE id_user = '"+ SessionName +"'";
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
        public ActionResult ConfirmOrderUser()
        {
            return View();
        }
    }
}