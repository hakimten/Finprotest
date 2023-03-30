using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finprotest.Models
{
    public class Ownerclass
    {
        public string Toko_long { get; internal set; }
        public string Toko_lat { get; internal set; }
        public string Toko_foto { get; internal set; }
        public string Toko_name { get; internal set; }
        public string Toko_nohp { get; internal set; }
        public string Toko_bank { get; internal set; }
        public string Toko_norek { get; internal set; }
        public string address_owner { get; internal set; }
        public string username_owner { get; internal set; }
        public string name_owner { get; internal set; }
        public int Toko_id { get; internal set; }
        public int id_owner { get; internal set; }
        public string REVISI { get; internal set; }
        public string Product_name { get; internal set; }
        public string product_img1 { get; internal set; }
        public string product_img2 { get; internal set; }
        public string product_img3 { get; internal set; }
        public string product_status { get; internal set; }
        public string Kategori_Name { get; internal set; }
        public string artist_name { get; internal set; }
        public int Product_id { get; internal set; }
        public int Kategori_ID { get; internal set; }
        public int product_harga { get; internal set; }
        public int Product_stock { get; internal set; }
        public int artist_ID { get; internal set; }
        public string Deskripsi_details { get; internal set; }
        public string Deskripsi_isi { get; internal set; }
        public string Deskripsi_kelebihan { get; internal set; }
        public int Deskripsi_id { get; internal set; }
        public string img_name { get; internal set; }
        public int Img_id { get; internal set; }
    }
}