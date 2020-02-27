using API.Models;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace API.Repositories
{
    public class SiteRepository : GeneralRepository<Site>
    {
        public SiteRepository(MySQLDatabase mySQL) : base(mySQL) { }
    }    
}
