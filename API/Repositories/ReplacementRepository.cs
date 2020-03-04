using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ReplacementRepository : GeneralRepository<Replacement>
    {
        public ReplacementRepository(MySQLDatabase mySQL) : base(mySQL) { }
    }
}
