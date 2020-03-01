using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class InterviewVM
    {
        public string InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public string Note { get; set; }   
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string SiteName { get; set; }
        public string Status { get; set; }
        public string IStatus { get; set; }
        public int EId { get; set; }
    }
}
