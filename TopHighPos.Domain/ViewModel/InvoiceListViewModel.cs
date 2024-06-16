using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain.ViewModel
{
    public class InvoiceListViewModel
    {
        public int ID { get; set; }
        public Guid odernumber { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
    }
}
