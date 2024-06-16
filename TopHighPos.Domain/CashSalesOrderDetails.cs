using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class CashSalesOrderDetails
    {
        public int oderdetailsid { get; set; }
        public Nullable<System.Guid> odernumber { get; set; }
        public string product { get; set; }
        public Nullable<double> qty { get; set; }
        public Nullable<double> salesprice { get; set; }
        public Nullable<double> unitcost { get; set; }
        public Nullable<double> totals { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;
    }
}
