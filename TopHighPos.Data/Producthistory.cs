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
    
    public partial class Producthistory
    {
        public long histid { get; set; }
        public Nullable<int> proid { get; set; }
        public Nullable<double> qty_in { get; set; }
        public Nullable<double> qty_out { get; set; }
        public Nullable<int> shopid { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
