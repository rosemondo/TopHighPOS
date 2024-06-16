using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class Suppliers : Contacts
    {
        public int supid { get; set; }
        [Required(ErrorMessage = "Enter customer name")]
        public string suppliername { get; set; }
        public string supaddress { get; set; }
    }
}
