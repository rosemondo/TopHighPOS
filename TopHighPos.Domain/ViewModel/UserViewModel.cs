using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain.ViewModel
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public int rolesid { get; set; }
        public int shopid { get; set; }
        public string roles { get; set; }
        public string locations { get; set; }
        public DateTime createddate { get; set; } = DateTime.Now;
        public DateTime lastupdateddate { get; set; } = DateTime.Now;
    }
}
