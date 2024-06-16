using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain.ViewModel
{
    public class ProductViewModel
    {
        public int proid { get; set; }
        public Nullable<int> catid { get; set; }
        public string categories { get; set; }
        [Required(ErrorMessage = "Enter product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Enter product description")]
        public string ProductDescription { get; set; }
        public string Sku { get; set; }
        public double Qty { get; set; }
        public Nullable<double> qty_in { get; set; }
        public Nullable<double> qty_out { get; set; }
        public Nullable<int> shopid { get; set; }
        public Nullable<int> supid { get; set; }
        public double UnitCost { get; set; }
        public double SalesPrice { get; set; }
        public double ReoderPoint { get; set; }
        public int coaid { get; set; }
        public int coaid2 { get; set; }
        public string cash_code { get; set; } = Guid.NewGuid().ToString();
        public Nullable<System.DateTime> jv_date { get; set; } = DateTime.Now.Date;
        public Nullable<double> debit { get; set; }
        public Nullable<double> credit { get; set; }
        public string descriptions { get; set; }
        public Nullable<System.TimeSpan> ent_time { get; set; } = DateTime.Now.TimeOfDay;
        public Nullable<int> RolesId { get; set; }
        public Nullable<System.DateTime> manufacturingdate { get; set; }
        public Nullable<System.DateTime> expirydate { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;
    }
}
