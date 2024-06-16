using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class Contacts
    {
        public string city { get; set; }
        public string state { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;
    }
}
