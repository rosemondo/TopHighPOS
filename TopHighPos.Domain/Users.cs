using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class Users
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Enter correct email address")]
        public string email_address { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string password { get; set; }
        public int rolesid { get; set; }
        public int shopid { get; set; }
        public Nullable<System.DateTime> createddate { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> lastupdateddate { get; set; } = DateTime.Now;
    }
}
