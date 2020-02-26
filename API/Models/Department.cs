using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Department
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Division")]
        public string DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
