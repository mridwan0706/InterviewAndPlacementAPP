using API.Models;
using API.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GeneralRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly MySQLDatabase database;
        public DynamicParameters parameters = new DynamicParameters();

        public GeneralRepository(MySQLDatabase mySQL)
        {
            database = mySQL;
        }

        public Task<int> Delete(int id)
        {
            var query = "Delete" + typeof(TEntity).Name;
            parameters.Add("i", id);
            var delete = database.Connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return delete;
        }

        public IEnumerable<TEntity> Get()
        {
            var query = "GetAll" + typeof(TEntity).Name + "s";
            var get = database.Connection.QueryAsync<TEntity>(query, commandType: CommandType.StoredProcedure).Result.ToList();
            return get;
        }

        public TEntity Get(int id)
        {
            var query = "Get" + typeof(TEntity).Name;
            parameters.Add("i", id);
            var get = database.Connection.QueryAsync<TEntity>(query, parameters, commandType: CommandType.StoredProcedure).Result.SingleOrDefault();
            return get;
        }

        public Task<int> Post(TEntity entity)
        {
            var query = "Insert" + typeof(TEntity).Name;
            parameters.AddDynamicParams(entity);
            var post = database.Connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return post;
        }

        public Task<int> Put(int id, TEntity entity)
        {
            var query = "Update" + typeof(TEntity).Name;
            parameters.Add("i", id);
            parameters.AddDynamicParams(entity);
            var put = database.Connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return put;
        }
    }
}
