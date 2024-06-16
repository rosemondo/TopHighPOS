using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class Basket
    {
        public int Id { get; set; }
        public string product { get; set; }
        public Nullable<double> qty { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<double> cost { get; set; }
        public Nullable<System.Guid> basketid { get; set; }
        public Nullable<int> proid { get; set; }
    }
}
