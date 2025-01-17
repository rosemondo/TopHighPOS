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
    
    public partial class CashSalesOder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashSalesOder()
        {
            this.CashSalesOrderDetails = new HashSet<CashSalesOrderDetail>();
        }
    
        public int oderid { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public System.Guid odernumber { get; set; }
        public string pay_meth { get; set; }
        public Nullable<double> subtotal { get; set; }
        public Nullable<double> vat { get; set; }
        public Nullable<double> sale_disc { get; set; }
        public Nullable<double> nettotal { get; set; }
        public Nullable<double> amt_rece { get; set; }
        public Nullable<double> amt_change { get; set; }
        public Nullable<int> custid { get; set; }
        public string salesagent { get; set; }
        public Nullable<int> shopid { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> lastupdatedate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Shop Shop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashSalesOrderDetail> CashSalesOrderDetails { get; set; }
    }
}
