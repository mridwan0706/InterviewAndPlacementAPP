using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_T_Placements")]
    public class Placement : Base
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //[ForeignKey("Site")]
        public int SiteId { get; set; }
        //public Site Site { get; set; }
        public int EmployeeId { get; set; }
    }    
}
