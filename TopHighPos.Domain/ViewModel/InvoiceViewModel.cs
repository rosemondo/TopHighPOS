using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain.ViewModel
{
    public class InvoiceViewModel
    {
        public int oderid { get; set; }
        public DateTime orderdate { get; set; }
        public Guid odernumber { get; set; }
        public string pay_meth { get; set; }
        public double subtotal { get; set; }
        public double vat { get; set; }
        public double nettotal { get; set; }
        public int po_num { get; set; }
        public DateTime due_date { get; set; }
        public int custid { get; set; }
        public string shopn_name { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string location { get; set; }
        public string salesagent { get; set; }
        public string customername { get; set; }
        public string custaddress { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public List<InvoiceOrderDetailsViewModel> InvoiceDetails { get; set; }
    }
}
