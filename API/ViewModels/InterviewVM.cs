using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class InterviewVM
    {
        //VM Tb_t_Interview
        public int Id { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int Interval { get; set; }
        //VM tb_m_Sites
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteEmail { get; set; }
        public string SitePhone { get; set; }
        public string SiteAddress { get; set; }
        public string PIC { get; set; }
        public string Logo { get; set; }
        //VM tb_m_Employee
        public string ParticipantId { get; set; }
        public string Participant { get; set; }
    }
}
