using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TopHighPos.Domain
{
    public class Customers : Contacts
    {
        public int custid { get; set; }
        [Required(ErrorMessage ="Enter customer name")]
        public string customername { get; set; }
        public string custaddress { get; set; }

        public IEnumerable<SelectListItem> CustomerSelectListItem { get; set; }
    }
}
