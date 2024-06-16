using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopHighPos.Domain.ViewModel;

namespace TopHighPos.Domain.ViewModel
{
    public class POSReceiptViewModel
    {
        public int orderid { get; set; }
        public Guid odernumber { get; set; }
        public DateTime orderdate { get; set; }
        public string CustomerName { get; set; }
        public string pay_meth { get; set; }
        public double subtotal { get; set; }
        public double vat { get; set; }
        public double sale_disc { get; set; }
        public double nettotal { get; set; }
        public double amt_rece { get; set; }
        public double amt_change { get; set; }
        public int custid { get; set; }
        public string shopn_name { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string location { get; set; }
        public string salesagent { get; set; }
        public List<ReceiptOrderDetailsViewModel> ReceiptDetails { get; set; }
    }
}
