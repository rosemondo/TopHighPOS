//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TopHighPos.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class JournalVoucher
    {
        public long jv_id { get; set; }
        public string cash_code { get; set; }
        public Nullable<System.DateTime> jv_date { get; set; }
        public int coaid { get; set; }
        public Nullable<double> debit { get; set; }
        public Nullable<double> credit { get; set; }
        public string descriptions { get; set; }
        public Nullable<System.TimeSpan> ent_time { get; set; }
        public Nullable<int> shopid { get; set; }
        public Nullable<int> RolesId { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> lastupdateddate { get; set; }
    
        public virtual ChartOfAccount ChartOfAccount { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
