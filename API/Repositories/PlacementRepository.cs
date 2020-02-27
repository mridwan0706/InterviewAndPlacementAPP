using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class PlacementRepository : GeneralRepository<Placement>
    {
        public PlacementRepository(MySQLDatabase mySQL) : base(mySQL) { }
    }
}
