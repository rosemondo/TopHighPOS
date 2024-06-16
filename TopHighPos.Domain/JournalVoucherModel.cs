using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class JournalVoucherModel
    {
        public long jv_id { get; set; }
        public string cash_code { get; set; }
        public Nullable<System.DateTime> jv_date { get; set; } = DateTime.Now.Date;
        public int coaid { get; set; }
        public Nullable<double> debit { get; set; }
        public Nullable<double> credit { get; set; }
        public string descriptions { get; set; }
        public Nullable<System.TimeSpan> ent_time { get; set; } = DateTime.Now.TimeOfDay;
        public Nullable<int> shopid { get; set; }
        public Nullable<int> RolesId { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;
    }
}
