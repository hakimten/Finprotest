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
    }
}