using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels;

namespace API.Repositories
{
    public class PlacementRepository : GeneralRepository<Placement>
    {
        private MySQLDatabase database;
        public PlacementRepository(MySQLDatabase mySQL) : base(mySQL) {
            database = mySQL;
        }

        public async Task<IEnumerable<PlacementVM>> GetPlacementSites()
        {
            var query = "SP_GetQuantity";
            var sites = await database.Connection.QueryAsync<PlacementVM>(query, commandType: CommandType.StoredProcedure);
            return sites;
        }

        public async Task<IEnumerable<PlacementVM>> DetailPlacementSite(int siteId)
        {
            var query = "SP_GetDetailQuantity";
            parameters.Add("idsite", siteId);
            var detail = await database.Connection.QueryAsync<PlacementVM>(query, parameters, commandType: CommandType.StoredProcedure);
            return detail;
        }
                
        public async Task<IEnumerable<PlacementVM>> HistoryUser(string empId)
        {
            var query = "SP_HistoryPlacements";
            parameters.Add("employeeid", empId);
            var placement = await database.Connection.QueryAsync<PlacementVM>(query, parameters, commandType: CommandType.StoredProcedure);
            return placement;
        }
    }
}
