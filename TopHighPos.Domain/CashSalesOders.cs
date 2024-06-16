using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class CashSalesOders
    {
        public int oderid { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; } = DateTime.Now.Date;
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
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdatedate { get; set; } = DateTime.Now;
    }
}
