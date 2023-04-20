using Finprotest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finprotest.Controllers
{
    public class OwnerController : Controller
    {
        string Mainconn = ConfigurationManager.ConnectionStrings["Finpro"].ConnectionString;
        //string Mainconn = ConfigurationManager.ConnectionStrings["Finpropc"].ConnectionString;
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
                string SessionName = Session["id_owner"].ToString();
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "SELECT t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.Kategori_Name, t3.name_owner, t4.artist_name  FROM Product_owner t1 JOIN KategoriProduct t2 ON t1.Kategori_ID = t2.Kategori_ID JOIN account_owner t3 ON t1.id_owner = t3.id_owner JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID WHERE t1.id_owner = '" + SessionName + "'";
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
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.name_owner = Convert.ToString(dr["name_owner"]);
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
                String sqlquery2 = "SELECT t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.Kategori_Name, t3.name_owner, t4.artist_name, t5.Deskripsi_id, t5.Deskripsi_isi, t5.Deskripsi_details, t5.Deskripsi_kelebihan FROM Product_owner t1 JOIN KategoriProduct t2 ON t1.Kategori_ID = t2.Kategori_ID JOIN account_owner t3 ON t1.id_owner = t3.id_owner JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Deskripsi_product t5 ON t1.Product_id = t5.Product_id  WHERE t1.id_owner = '" + SessionName + "'";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<Ownerclass> uc2 = new List<Ownerclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
                    {
                        Ownerclass uc10 = new Ownerclass();
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.name_owner = Convert.ToString(dr["name_owner"]);
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.Deskripsi_details = Convert.ToString(dr["Deskripsi_details"]);
                        uc10.Deskripsi_isi = Convert.ToString(dr["Deskripsi_isi"]);
                        uc10.Deskripsi_kelebihan = Convert.ToString(dr["Deskripsi_kelebihan"]);
                        uc10.Deskripsi_id = Convert.ToInt32(dr["Deskripsi_id"]);
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
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult Product2(int id)
        {
            if (Session["id_owner"] != null)
            {
                string SessionName = Session["id_owner"].ToString();
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = $"SELECT t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.Kategori_Name, t3.name_owner, t4.artist_name, t5.Deskripsi_id, t5.Deskripsi_isi, t5.Deskripsi_details, t5.Deskripsi_kelebihan FROM Product_owner t1 JOIN KategoriProduct t2 ON t1.Kategori_ID = t2.Kategori_ID JOIN account_owner t3 ON t1.id_owner = t3.id_owner JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID JOIN Deskripsi_product t5 ON t1.Product_id = t5.Product_id WHERE t1.Product_id = {id}";
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
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.name_owner = Convert.ToString(dr["name_owner"]);
                        uc10.artist_name = Convert.ToString(dr["artist_name"]);
                        uc10.Deskripsi_details = Convert.ToString(dr["Deskripsi_details"]);
                        uc10.Deskripsi_isi = Convert.ToString(dr["Deskripsi_isi"]);
                        uc10.Deskripsi_kelebihan = Convert.ToString(dr["Deskripsi_kelebihan"]);
                        uc10.Deskripsi_id = Convert.ToInt32(dr["Deskripsi_id"]);
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
                String sqlquery2 = $"SELECT * from Image_product WHERE Product_id = {id}";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<userclass> uc2 = new List<userclass>();
                {

                    foreach (DataRow dr in ds2.Rows)
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
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        [HttpPost]
        public ActionResult AddProductOwner(FormCollection form, HttpPostedFileBase frontimg, HttpPostedFileBase sideimg, HttpPostedFileBase backimg)
        {
            if (Session["id_owner"] != null || Session["username_owner"] != null)
            {
                SqlConnection myConnection = new SqlConnection();
                string SessionName = Session["id_owner"].ToString();
                string SessionName2 = Session["username_owner"].ToString();
                string productname = form["productname"];
                string artist = form["artist"];
                string kategori = form["kategori"];
                string price = form["price"];
                string stock = form["stock"];
                string weight = form["weight"];

                string sewnPatternImage = "FrontImageproduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(frontimg.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/FrontImage/"), sewnPatternImage);
                frontimg.SaveAs(filePathsewnPattern);

                string sewnPatternImage3 = "BackImageproduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(backimg.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern3 = Path.Combine(Server.MapPath("/Upload/BackImg/"), sewnPatternImage3);
                backimg.SaveAs(filePathsewnPattern3);

                string sewnPatternImage2 = "SideImageproduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(sideimg.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern2 = Path.Combine(Server.MapPath("/Upload/SideImg/"), sewnPatternImage2);
                sideimg.SaveAs(filePathsewnPattern2);

                myConnection.ConnectionString = Mainconn;
                string Query3 = "INSERT INTO Product_owner (Kategori_ID, Product_name, product_img1, product_img2, product_img3, id_owner, product_harga, Product_stock, artist_ID, product_status, Berat) VALUES (@Kategori_ID, @Product_name, @product_img1, @product_img2, @product_img3, @id_owner, @product_harga, @Product_stock, @artist_ID,'B', @Berat)";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
                {
                    sqlmethod.Parameters.AddWithValue("@Kategori_ID", kategori);
                    sqlmethod.Parameters.AddWithValue("@Product_name", productname);
                    sqlmethod.Parameters.AddWithValue("@product_img1", sewnPatternImage);
                    sqlmethod.Parameters.AddWithValue("@product_img2", sewnPatternImage2);
                    sqlmethod.Parameters.AddWithValue("@product_img3", sewnPatternImage3);
                    sqlmethod.Parameters.AddWithValue("@id_owner", SessionName);
                    sqlmethod.Parameters.AddWithValue("@product_harga", price);
                    sqlmethod.Parameters.AddWithValue("@Product_stock", stock);
                    sqlmethod.Parameters.AddWithValue("@artist_ID", artist);
                    sqlmethod.Parameters.AddWithValue("@Berat", weight);
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                return RedirectToAction("Product");
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        [HttpPost]
        public ActionResult AddDeskripsiOwner(FormCollection form, IEnumerable<HttpPostedFileBase> frontimg)
        {
            if (Session["id_owner"] != null || Session["username_owner"] != null)
            {
                SqlConnection myConnection = new SqlConnection();
                string SessionName = Session["id_owner"].ToString();
                string SessionName2 = Session["username_owner"].ToString();
                string deskripsiproduct = form["deskripsiproduct"];
                string deskripsidetils = form["deskripsidetils"];
                string kelebihan = form["kelebihan"];
                string idproduct = form["idproduct"];

                foreach (var ImageSave in frontimg)
                {
                    if (ImageSave == null)
                    {

                    }
                    else if (ImageSave != null)
                    {
                        myConnection.ConnectionString = Mainconn;
                        string sewnPatternImage = "ImageProduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetFileName(ImageSave.FileName);
                        //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                        string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/ImageProduct/"), sewnPatternImage);
                        ImageSave.SaveAs(filePathsewnPattern);

                        string Query3 = "INSERT INTO Image_product (img_name, img_numb, Product_id) VALUES (@img_name, '0', @Product_id)";
                        using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
                        {
                            sqlmethod.Parameters.AddWithValue("@img_name", sewnPatternImage);
                            sqlmethod.Parameters.AddWithValue("@Product_id", idproduct);
                            myConnection.Open();
                            sqlmethod.ExecuteNonQuery();
                            TempData["messsage"] = "success";
                            myConnection.Close();
                        }
                    }
                }
                myConnection.ConnectionString = Mainconn;
                string Query1 = "INSERT INTO Deskripsi_product (Deskripsi_isi, Deskripsi_details, Deskripsi_kelebihan, Product_id) VALUES (@Deskripsi_isi, @Deskripsi_details, @Deskripsi_kelebihan, @Product_id)";
                using (SqlCommand sqlmethod = new SqlCommand(Query1, myConnection))
                {
                    sqlmethod.Parameters.AddWithValue("@Deskripsi_isi", deskripsiproduct);
                    sqlmethod.Parameters.AddWithValue("@Deskripsi_details", deskripsidetils);
                    sqlmethod.Parameters.AddWithValue("@Deskripsi_kelebihan", kelebihan);
                    sqlmethod.Parameters.AddWithValue("@Product_id", idproduct);
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                string Query5 = "UPDATE Product_owner SET product_status ='A' WHERE Product_id = '" + idproduct + "'";
                using (SqlCommand sqlmethod = new SqlCommand(Query5, myConnection))
                {
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                return RedirectToAction("Product");
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        [HttpPost]
        public ActionResult EditProductOwner(FormCollection form, HttpPostedFileBase frontimg, HttpPostedFileBase sideimg, HttpPostedFileBase backimg)
        {
            if (Session["id_owner"] != null || Session["username_owner"] != null)
            {
                SqlConnection myConnection = new SqlConnection();
                string SessionName = Session["id_owner"].ToString();
                string SessionName2 = Session["username_owner"].ToString();
                string productname = form["productname"];
                string artist = form["artist"];
                string kategori = form["kategori"];
                string price = form["price"];
                string stock = form["stock"];
                string idproduct = form["idproduct"];

                string sewnPatternImage = "FrontImageproduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(frontimg.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/FrontImage/"), sewnPatternImage);
                frontimg.SaveAs(filePathsewnPattern);

                string sewnPatternImage2 = "SideImageproduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(sideimg.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern2 = Path.Combine(Server.MapPath("/Upload/SideImg/"), sewnPatternImage2);
                sideimg.SaveAs(filePathsewnPattern2);

                string sewnPatternImage3 = "BackImageproduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(backimg.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern3 = Path.Combine(Server.MapPath("/Upload/BackImg/"), sewnPatternImage3);
                backimg.SaveAs(filePathsewnPattern3);

                myConnection.ConnectionString = Mainconn;
                string Query3 = "UPDATE Product_owner SET Kategori_ID=@Kategori_ID, Product_name=@Product_name, product_img1=@product_img1,product_img2=@product_img2,product_img3=@product_img3, id_owner=@id_owner, product_harga=@product_harga, Product_stock=@Product_stock, artist_ID=@artist_ID WHERE Product_id = @Product_id";
                using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
                {
                    sqlmethod.Parameters.AddWithValue("@Kategori_ID", kategori);
                    sqlmethod.Parameters.AddWithValue("@Product_name", productname);
                    sqlmethod.Parameters.AddWithValue("@product_img1", sewnPatternImage);
                    sqlmethod.Parameters.AddWithValue("@product_img2", sewnPatternImage2);
                    sqlmethod.Parameters.AddWithValue("@product_img3", sewnPatternImage3);
                    sqlmethod.Parameters.AddWithValue("@id_owner", SessionName);
                    sqlmethod.Parameters.AddWithValue("@product_harga", price);
                    sqlmethod.Parameters.AddWithValue("@Product_stock", stock);
                    sqlmethod.Parameters.AddWithValue("@artist_ID", artist);
                    sqlmethod.Parameters.AddWithValue("@Product_id", idproduct);
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                return RedirectToAction("Product");
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        [HttpPost]
        public ActionResult EditDeskripsiOwner(FormCollection form, IEnumerable<HttpPostedFileBase> frontimg2, List<string> idfoto)
        {
            if (Session["id_owner"] != null || Session["username_owner"] != null)
            {
                SqlConnection myConnection = new SqlConnection();
                string SessionName = Session["id_owner"].ToString();
                string SessionName2 = Session["username_owner"].ToString();
                string deskripsiproduct = form["deskripsiproduct"];
                string deskripsidetils = form["deskripsidetils"];
                string kelebihan = form["kelebihan"];
                string idProduct = form["idProduct"];

                foreach (var ImageSave2 in frontimg2)
                {
                    if (ImageSave2 == null)
                    {

                    }
                    else if (ImageSave2 != null)
                    {

                        myConnection.ConnectionString = Mainconn;
                        string varString2 = "";
                        for (int kk = 0; kk < idfoto.Count(); kk++)
                        {
                            varString2 += $"DELETE FROM Image_product WHERE Img_id = '" + idfoto[kk] + "';";
                        }
                        using (SqlCommand sqlmethod = new SqlCommand(varString2, myConnection))
                        {
                            myConnection.Open();
                            sqlmethod.ExecuteNonQuery();
                            TempData["messsage"] = "success";
                            myConnection.Close();
                        }

                        string sewnPatternImage = "ImageProduct" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetFileName(ImageSave2.FileName);
                        //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                        string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/ImageProduct/"), sewnPatternImage);
                        ImageSave2.SaveAs(filePathsewnPattern);

                        string Query3 = "INSERT INTO Image_product (img_name, img_numb, Product_id) VALUES (@img_name, '0', @Product_id)";
                        using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
                        {
                            sqlmethod.Parameters.AddWithValue("@img_name", sewnPatternImage);
                            sqlmethod.Parameters.AddWithValue("@Product_id", idProduct);
                            myConnection.Open();
                            sqlmethod.ExecuteNonQuery();
                            TempData["messsage"] = "success";
                            myConnection.Close();
                        }
                    }
                }
                myConnection.ConnectionString = Mainconn;
                string Query1 = "UPDATE Deskripsi_product SET Deskripsi_isi=@Deskripsi_isi, Deskripsi_details=@Deskripsi_details, Deskripsi_kelebihan=@Deskripsi_kelebihan WHERE Product_id=@Product_id";
                using (SqlCommand sqlmethod = new SqlCommand(Query1, myConnection))
                {
                    sqlmethod.Parameters.AddWithValue("@Deskripsi_isi", deskripsiproduct);
                    sqlmethod.Parameters.AddWithValue("@Deskripsi_details", deskripsidetils);
                    sqlmethod.Parameters.AddWithValue("@Deskripsi_kelebihan", kelebihan);
                    sqlmethod.Parameters.AddWithValue("@Product_id", idProduct);
                    myConnection.Open();
                    sqlmethod.ExecuteNonQuery();
                    TempData["messsage"] = "success";
                    myConnection.Close();
                }
                return RedirectToAction("Product");
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult DetailsProduct(int id2)
        {
            // display product owner 
            SqlConnection sqlconn = new SqlConnection(Mainconn);
            String sqlquery2 = $"SELECT FORMAT(product_harga, 'N') AS RP, t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.address_owner, t3.Kategori_Name, t4.artist_name FROM Product_owner t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner JOIN KategoriProduct t3 ON t1.Kategori_ID = t3.Kategori_ID JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID WHERE t1.Product_id = {id2} AND t1.product_status = 'A'";
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
            String sqlquery = $"SELECT * from Image_product WHERE Product_id = {id2}";
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
            //menampilkan lattitude & longitude Toko
            String sqlquery3 = $"SELECT * from Deskripsi_product WHERE Product_id = {id2}";
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
        public ActionResult Product_soldout()
        {
            if (Session["id_owner"] != null)
            {
                string SessionName = Session["id_owner"].ToString();
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "SELECT t1.Product_id, t1.Kategori_ID, t1.Product_name, t1.product_img1, t1.product_img2, t1.product_img3, t1.id_owner, t1.product_harga, t1.Product_stock, t1.artist_ID, t1.product_status, t2.Kategori_Name, t3.name_owner, t4.artist_name  FROM Product_owner t1 JOIN KategoriProduct t2 ON t1.Kategori_ID = t2.Kategori_ID JOIN account_owner t3 ON t1.id_owner = t3.id_owner JOIN artist_Db t4 ON t1.artist_ID = t4.artist_ID WHERE t1.id_owner = '" + SessionName + "' AND t1.Product_stock = '0'";
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
                        uc10.Product_name = Convert.ToString(dr["Product_name"]);
                        uc10.product_img1 = Convert.ToString(dr["product_img1"]);
                        uc10.product_img2 = Convert.ToString(dr["product_img2"]);
                        uc10.product_img3 = Convert.ToString(dr["product_img3"]);
                        uc10.product_status = Convert.ToString(dr["product_status"]);
                        uc10.Kategori_Name = Convert.ToString(dr["Kategori_Name"]);
                        uc10.name_owner = Convert.ToString(dr["name_owner"]);
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
                ViewBag.uc = uc;
                sqlconn.Close();
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
                var products = GetProducts();
                var priorityProducts = products.Where(p => p.eks_name == "PREMIUM").OrderBy(p => p.updated_at);
                var standardProducts = products.Where(p => p.eks_name == "STANDAR").OrderBy(p => p.updated_at);
                var model = priorityProducts.Concat(standardProducts);

                return View(model);
            }
            else
            {
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        private List<Ownerclass> GetProducts()
        {
            List<Ownerclass> persons = new List<Ownerclass>();
            using (SqlConnection connection = new SqlConnection(Mainconn))
            {
                string session1 = Session["id_owner"].ToString();
                connection.Open();
                string query = "SELECT t1.cout_id, t1.all_total, t1.eks_id, t1.pay_ID, t1.id_user, t1.id_owner, t1.almt_Id, t1.cout_status, t1.payment_history, t1.updated_at, t2.eks_name, t2.eks_harga, t3.payment_name, t4.alamt_name, t4.lattitude_user, t4.longitude_user, t4.no_hpbuy, t5.name_user FROM checkout_user t1 JOIN Ekspedis_c t2 ON t1.eks_id = t2.eks_id JOIN paymentMethod t3 ON t1.pay_ID = t3.pay_ID JOIN alamat_user t4 ON t1.almt_Id = t4.almt_Id JOIN account_user t5 ON t1.id_user = t5.id_user WHERE t1.id_owner = '"+ session1 + "' AND t1.cout_status = 'PAYMENT'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ownerclass person = new Ownerclass();
                    person.cout_id = (int)reader["cout_id"];
                    person.eks_harga = (int)reader["eks_harga"];
                    person.all_total = (int)reader["all_total"];
                    person.eks_id = (int)reader["eks_id"];
                    person.pay_ID = (int)reader["pay_ID"];
                    person.id_user = (int)reader["id_user"];
                    person.id_owner = (int)reader["id_owner"];
                    person.almt_Id = (int)reader["almt_Id"];
                    person.cout_status = (string)reader["cout_status"];
                    person.payment_name = (string)reader["payment_name"];
                    if (reader["payment_history"] == DBNull.Value)
                    {

                    }
                    else
                    {
                        person.payment_history = (string)reader["payment_history"];
                    }
                    person.eks_name = (string)reader["eks_name"];
                    person.alamt_name = (string)reader["alamt_name"];
                    person.lattitude_user = (string)reader["lattitude_user"];
                    person.longitude_user = (string)reader["longitude_user"];
                    person.no_hpbuy = (string)reader["no_hpbuy"];
                    person.name_user = (string)reader["name_user"];
                    person.updated_at = (DateTime)reader["updated_at"];
                    persons.Add(person);
                }
            }
            return persons;
        }
        public ActionResult Profil()
        {
            if (Session["id_owner"] != null)
            {
                string SessionName = Session["id_owner"].ToString();
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = "SELECT t1.Toko_id, t1.Toko_name, t1.Toko_foto, t1.id_owner, t1.Toko_long, t1.Toko_lat, t1.Toko_nohp, t1.Toko_bank, Toko_norek, t1.Toko_status, t2.name_owner, t2.username_owner, t2.address_owner FROM Toko_Profil t1 JOIN account_owner t2 ON t1.id_owner = t2.id_owner WHERE t1.id_owner = '" + SessionName + "'";
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
                string roomid = form["roomid"];

                string sewnPatternImage = "ProfilPicture" + "-" + DateTime.Now.ToString("(yyyyMMdd)(Hmmss)(tt)") + Path.GetExtension(profilpicture.FileName);
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + sewnPatternImage.Trim() + sewnPatternImage;
                string filePathsewnPattern = Path.Combine(Server.MapPath("/Upload/Profil/"), sewnPatternImage);
                profilpicture.SaveAs(filePathsewnPattern);

                myConnection.ConnectionString = Mainconn;
                string Query3 = "UPDATE Toko_Profil SET Toko_name=@Toko_name, Toko_foto=@Toko_foto, Toko_lat=@Toko_lat, Toko_nohp=@Toko_nohp, Toko_bank=@Toko_bank, Toko_norek=@Toko_norek WHERE Toko_id=@Toko_id";
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
                    sqlmethod.Parameters.AddWithValue("@Toko_id", roomid);
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
        public ActionResult History_order()
        {
            if (Session["id_owner"] != null)
            {
                string SessionName = Session["id_owner"].ToString();
                //menampilkan lattitude & longitude Toko
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                string sqlquery = $"SELECT * FROM checkout_user WHERE cout_status = 'FINISH'";
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
                    string sqlquery2 = $" SELECT t1.Cart_id, t1.Product_id, t1.Cart_kuantity, t1.total_harga, t1.id_user, t1.cout_id, t2.Product_name, t2.product_img1, t2.id_owner, t3.name_user FROM Cart_user t1 JOIN Product_owner t2 ON t1.Product_id = t2.Product_id JOIN account_user t3 ON t1.id_user = t3.id_user WHERE t1.cout_id = '{data.cout_id}' AND t1.id_owner = '" + SessionName + "'";
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
                        data2.name_user = (string)dr1["name_user"];
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
        public ActionResult EstimasiWaktu(int id)
        {
            if (Session["id_owner"] != null)
            {
                string SessionName = Session["id_owner"].ToString();
                SqlConnection sqlconn = new SqlConnection(Mainconn);
                String sqlquery = $"SELECT t1.cout_id, t1.all_total, t1.eks_id, t1.pay_ID, t1.id_user, t1.id_owner, t1.almt_Id, t1.cout_status, t1.payment_history, t1.updated_at, t2.eks_name, t2.eks_harga, t3.payment_name, t4.alamt_name, t4.lattitude_user, t4.longitude_user, t4.no_hpbuy, t5.name_user, t6.Toko_name, t6.Toko_long, t6.Toko_lat FROM checkout_user t1 JOIN Ekspedis_c t2 ON t1.eks_id = t2.eks_id JOIN paymentMethod t3 ON t1.pay_ID = t3.pay_ID JOIN alamat_user t4 ON t1.almt_Id = t4.almt_Id JOIN account_user t5 ON t1.id_user = t5.id_user JOIN Toko_Profil t6 ON t1.id_owner = t6.id_owner WHERE t1.cout_id = {id}";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                List<Ownerclass> uc = new List<Ownerclass>();
                {

                    foreach (DataRow reader in ds.Rows)
                    {
                        Ownerclass person = new Ownerclass();
                        person.cout_id = (int)reader["cout_id"];
                        person.eks_harga = (int)reader["eks_harga"];
                        person.all_total = (int)reader["all_total"];
                        person.eks_id = (int)reader["eks_id"];
                        person.pay_ID = (int)reader["pay_ID"];
                        person.id_user = (int)reader["id_user"];
                        person.id_owner = (int)reader["id_owner"];
                        person.almt_Id = (int)reader["almt_Id"];
                        person.cout_status = (string)reader["cout_status"];
                        person.payment_name = (string)reader["payment_name"];
                        if (reader["payment_history"] == DBNull.Value)
                        {

                        }
                        else
                        {
                            person.payment_history = (string)reader["payment_history"];
                        }
                        person.eks_name = (string)reader["eks_name"];
                        person.alamt_name = (string)reader["alamt_name"];
                        person.lattitude_user = (string)reader["lattitude_user"];
                        person.longitude_user = (string)reader["longitude_user"];
                        person.Toko_name = (string)reader["Toko_name"];
                        person.Toko_long = (string)reader["Toko_long"];
                        person.Toko_lat = (string)reader["Toko_lat"];
                        person.no_hpbuy = (string)reader["no_hpbuy"];
                        person.name_user = (string)reader["name_user"];
                        person.updated_at = (DateTime)reader["updated_at"];
                        uc.Add(person);
                    }
                }
                String sqlquery2 = $"SELECT COUNT (Cart_id) AS ITEM FROM Cart_user WHERE cout_id = {id}";
                SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm2);
                DataTable ds2 = new DataTable();
                sda2.Fill(ds2);
                List<Ownerclass> uc2 = new List<Ownerclass>();
                {

                    foreach (DataRow reader in ds2.Rows)
                    {
                        Ownerclass person = new Ownerclass();
                        person.ITEM = (int)reader["ITEM"];
                        uc2.Add(person);
                    }
                }
                String sqlquery3 = $"SELECT SUM(Cart_kuantity) AS QUANTITY FROM Cart_user WHERE cout_id = {id}";
                SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
                SqlDataAdapter sda3 = new SqlDataAdapter(sqlcomm3);
                DataTable ds3 = new DataTable();
                sda3.Fill(ds3);
                List<Ownerclass> uc3 = new List<Ownerclass>();
                {

                    foreach (DataRow reader in ds3.Rows)
                    {
                        Ownerclass person = new Ownerclass();
                        person.QUANTITY = (int)reader["QUANTITY"];
                        uc3.Add(person);
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
                return RedirectToAction("LoginOwner", "Login");
            }
        }
        public ActionResult ConfirmProductPremium(FormCollection form, double kmjarak, double waktupenyiapan, string paydate)
        {
            SqlConnection myConnection = new SqlConnection();
            string idcout = form["idcout"];
            string noresi = form["noresi"];

            string item = form["item"];
            double Itemcart = Convert.ToDouble(item);

            string kuantity = form["kuantity"];
            double Kuantitycart = Convert.ToDouble(kuantity);

            DateTime waktuPengiriman;
            if (!DateTime.TryParseExact(paydate, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out waktuPengiriman))
            {
                //Jika waktu yang dimasukkan tidak valid, maka gunakan waktu saat ini
                //waktuPengiriman = DateTime.Now;
                waktuPengiriman = DateTime.Now;
                //waktuPengiriman = Convert.ToDateTime(form["paydate"]);
            }
            waktuPengiriman = waktuPengiriman.AddHours(kmjarak * 3 + Itemcart * 2 + Kuantitycart * 2 +  waktupenyiapan);

            myConnection.ConnectionString = Mainconn;
            string Query3 = "INSERT Estimasi_waktu (cout_id, estimasi_sampai, no_resi, status, waktu_pengiriman) VALUES (@cout_id, @estimasi_sampai, @no_resi, 'DIKEMAS', CURRENT_TIMESTAMP)";
            using (SqlCommand sqlmethod = new SqlCommand(Query3, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@cout_id", idcout);
                sqlmethod.Parameters.AddWithValue("@estimasi_sampai", waktuPengiriman);
                sqlmethod.Parameters.AddWithValue("@no_resi", noresi);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            string Query4 = "UPDATE checkout_user SET cout_status = 'DIKEMAS' WHERE cout_id = @cout_id";
            using (SqlCommand sqlmethod = new SqlCommand(Query4, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@cout_id", idcout);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }
            string Query5 = "UPDATE Cart_user SET cart_status = 'DIKEMAS' WHERE cout_id = @cout_id";
            using (SqlCommand sqlmethod = new SqlCommand(Query5, myConnection))
            {
                sqlmethod.Parameters.AddWithValue("@cout_id", idcout);
                myConnection.Open();
                sqlmethod.ExecuteNonQuery();
                TempData["messsage"] = "success";
                myConnection.Close();
            }

            return RedirectToAction("Order_masuk");
        }
    }
}