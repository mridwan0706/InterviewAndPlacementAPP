using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_T_Interviews")]
    public class Interview : Base
    {
        public DateTime InterviewDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        //[ForeignKey("Site")]
        public int SiteId { get; set; }
        //public Site Site { get; set; }
    }
}
