using API.Databases;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class InterviewRepository : GeneralRepository<Interview>
    {
        public InterviewRepository(MySQLDatabase mySQL): base(mySQL) {}
    }
}
