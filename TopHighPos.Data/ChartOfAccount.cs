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
    
    public partial class ChartOfAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChartOfAccount()
        {
            this.JournalVouchers = new HashSet<JournalVoucher>();
        }
    
        public int coaid { get; set; }
        public int coa_code { get; set; }
        public string coa_name { get; set; }
        public string coa_group { get; set; }
        public string coa_sub_group { get; set; }
        public string coa_cate { get; set; }
        public string coa_cf { get; set; }
        public string coa_ocbfy { get; set; }
        public string coa_cogm { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> lastupdateddate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JournalVoucher> JournalVouchers { get; set; }
    }
}
