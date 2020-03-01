using API.Databases;
using API.Repositories.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GeneralRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly MySQLDatabase database;
        public DynamicParameters parameters = new DynamicParameters();

        public GeneralRepository(MySQLDatabase mySQL)
        {
            database = mySQL;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await database.Connection.GetAsync<TEntity>(id);
            var delete = await database.Connection.DeleteAsync(entity);
            return delete;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var getall = await database.Connection.GetAllAsync<TEntity>();
            return getall;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var get = await database.Connection.GetAsync<TEntity>(id);
            return get;
        }

        public async Task<int> PostAsync(TEntity entity)
        {
            var post = await database.Connection.InsertAsync(entity);
            return post;
        }

        public async Task<bool> PullAsync(TEntity entity)
        {
            var put = await database.Connection.UpdateAsync(entity);
            return put;
        }
    }
}
