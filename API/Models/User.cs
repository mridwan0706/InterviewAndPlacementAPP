using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User : IdentityUser
    {       
        [ForeignKey("Employee")]
        public override string Id { get; set; }
        public Employee Employee { get; set; }
    }
}
