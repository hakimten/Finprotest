//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finprotest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart_user
    {
        public int Cart_id { get; set; }
        public Nullable<int> Product_id { get; set; }
        public Nullable<int> Cart_kuantity { get; set; }
        public Nullable<int> total_harga { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> cout_id { get; set; }
        public string cart_status { get; set; }
        public Nullable<System.DateTime> tanggal_pembelian { get; set; }
        public Nullable<System.DateTime> tanggal_checkout { get; set; }
    }
}
