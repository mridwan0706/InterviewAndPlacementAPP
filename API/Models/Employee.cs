using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Employees")]
    public class Employee :Base
    {
        
        public string ParticipantId { get; set; }
        public string Participant { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }       

    }
}
