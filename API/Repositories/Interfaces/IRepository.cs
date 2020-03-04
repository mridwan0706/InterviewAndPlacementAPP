using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<int> PostAsync(T entity);
        Task<bool> PullAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
