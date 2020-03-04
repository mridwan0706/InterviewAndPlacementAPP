using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class InterviewVM
    {
        public string InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string SiteId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string SiteName { get; set; }
    }
}
