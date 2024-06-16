using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain.ViewModel
{
    public  class CreditSalesInvoiceViewModel
    {
        public int oderid { get; set; }
        public DateTime orderdate { get; set; }
        public Guid odernumber { get; set; }
        public string pay_meth { get; set; }
        public double subtotal { get; set; }
        public double vat { get; set; }
        public double sale_disc { get; set; }
        public double nettotal { get; set; }
        public int po_num { get; set; }
        public DateTime due_date { get; set; }
        public int custid { get; set; }
        public string salesagent { get; set; }
        public int shopid { get; set; }
        public List<CreditSalesDetailsViewModel> CreditSalesOrderDetails { get; set; }
    }
}
