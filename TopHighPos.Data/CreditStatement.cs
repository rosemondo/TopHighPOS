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
    
    public partial class CreditStatement
    {
        public long stid { get; set; }
        public Nullable<System.DateTime> st_date { get; set; }
        public string description { get; set; }
        public string vch_type { get; set; }
        public Nullable<double> debit { get; set; }
        public Nullable<double> credit { get; set; }
        public int custid { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> lastupdatedate { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}