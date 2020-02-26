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

    //public class SiteRepository : ISiteRepository
    //{
    //    private readonly MySQLDatabase database;
    //    public DynamicParameters parameters = new DynamicParameters();

    //    public SiteRepository(MySQLDatabase mySQL)
    //    {
    //        database = mySQL;
    //    }

    //    public async Task<int> Delete(int id)
    //    {
    //        var query = "DeleteSite";
    //        parameters.Add("i", id);            
    //        var affectedRow = await database.Connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
    //        return affectedRow;
    //    }

    //    public IEnumerable<Site> Get()
    //    {
    //        var sites = database.Connection.QueryAsync<Site>("Call GetAllSites()").Result.ToList();
    //        return sites;
    //    }

    //    public Site Get(int id)
    //    {
    //        var query = "GetSite";
    //        parameters.Add("i", id);
    //        var sites = database.Connection.QueryAsync<Site>(query, parameters, commandType: CommandType.StoredProcedure).Result.SingleOrDefault();
    //        return sites;
    //    }

    //    public async Task<int> Post(Site site)
    //    {
    //        var query = "InsertSite";
    //        parameters.AddDynamicParams(site);
    //        var affectedRow = await database.Connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
    //        return affectedRow;
    //    }

    //    public async Task<int> Put(int id, Site site)
    //    {
    //        var query = "UpdateSite";
    //        parameters.Add("i", id);
    //        parameters.AddDynamicParams(site);
    //        var affectedRow = await database.Connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
    //        return affectedRow;
    //    }
    //}
}
