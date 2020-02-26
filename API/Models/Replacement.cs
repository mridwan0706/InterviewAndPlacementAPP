using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_T_Replacements")]
    public class Replacement : Base
    {
        public DateTime RequestDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        [ForeignKey("Placement")]
        public int PlacementId { get; set; }
        public Placement Placement { get; set; }

        public Replacement()
        {
            CreateDate = DateTime.Now.ToLocalTime();
        }   
    }
}
