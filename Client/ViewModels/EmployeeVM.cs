using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        public string ParticipantId { get; set; }
        public string Participant { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
