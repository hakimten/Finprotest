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
    
    public partial class account_owner
    {
        public int id_owner { get; set; }
        public string name_owner { get; set; }
        public string username_owner { get; set; }
        public string password_owner { get; set; }
        public string address_owner { get; set; }
        public Nullable<int> phone { get; set; }
        public string gender_owner { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> update_at { get; set; }
    }
}
