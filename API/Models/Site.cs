using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Sites")]
    public class Site : Base
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PIC { get; set; }
        public string Logo { get; set; }
        public Site()
        {
            CreateDate = DateTime.Now.ToLocalTime();
        }
    }
}
