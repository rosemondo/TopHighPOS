﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain.ViewModel
{
    public class CreditSalesDetailsViewModel
    {
        public int oderdetailsid { get; set; }
        public Guid odernumber { get; set; }
        public string product { get; set; }
        public double qty { get; set; }
        public double salesprice { get; set; }
        public double unitcost { get; set; }
        public double totals { get; set; }
    }
}
