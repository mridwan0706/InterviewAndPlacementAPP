using API.Databases;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public class SiteRepository:GeneralRepository<Site>
    {
        public SiteRepository(MySQLDatabase mySQL) : base(mySQL) { }
    }
}
