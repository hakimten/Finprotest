using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finprotest.Models
{
    public class Adminclass
    {
        public string RP { get; internal set; }
        public string Product_name { get; internal set; }
        public string Toko_name { get; internal set; }
        public string product_img1 { get; internal set; }
        public string product_img2 { get; internal set; }
        public string product_img3 { get; internal set; }
        public string product_status { get; internal set; }
        public string address_owner { get; internal set; }
        public string Kategori_Name { get; internal set; }
        public string artist_name { get; internal set; }
        public int Product_id { get; internal set; }
        public int Kategori_ID { get; internal set; }
        public int id_owner { get; internal set; }
        public int product_harga { get; internal set; }
        public int Product_stock { get; internal set; }
        public int artist_ID { get; internal set; }
        public int Toko_id { get; internal set; }
        public string username_owner { get; internal set; }
        public string name_owner { get; internal set; }
        public int id_user { get; internal set; }
        public string name_user { get; internal set; }
        public string email_user { get; internal set; }
        public string username_user { get; internal set; }
    }
}