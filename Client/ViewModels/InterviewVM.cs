using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class InterviewVM
    {
        public int Id { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public SiteVM Site { get; set; }
        public EmployeeVM Employee { get; set; }
    }
}
