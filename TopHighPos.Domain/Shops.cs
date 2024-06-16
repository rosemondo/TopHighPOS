using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class Shops
    {
        public int shopid { get; set; }
        public string shopn_name { get; set; }
        public string shop_address { get; set; }
        public string shop_city { get; set; }
        public string shop_state { get; set; }
        public string shop_phone { get; set; }
        public string shop_email { get; set; }
        public string shop_website { get; set; }
        public string shop_location { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;
    }
}
