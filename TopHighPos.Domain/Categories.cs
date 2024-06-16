using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopHighPos.Domain
{
    public class Categories
    {
        public int catid { get; set; }
        [Required(ErrorMessage = "Enter category name")]
        public string cate { get; set; }
        public System.DateTime createddate { get; set; } = DateTime.Now;
        public System.DateTime lastupdateddate { get; set; } = DateTime.Now;
    }
}
