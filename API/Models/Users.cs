using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Users")]
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
