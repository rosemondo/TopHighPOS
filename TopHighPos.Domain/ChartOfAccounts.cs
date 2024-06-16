using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TopHighPos.Domain
{
    public class ChartOfAccounts
    {
        public int coaid { get; set; }
        public int coa_code { get; set; }
        public string coa_name { get; set; }
        public string coa_group { get; set; }
        public string coa_sub_group { get; set; }
        public string coa_cate { get; set; }
        public string coa_cf { get; set; }
        public string coa_ocbfy { get; set; }
        public string coa_cogm { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;

        public IEnumerable<SelectListItem> ChartAccSelectListItem { get; set; }
    }
}
