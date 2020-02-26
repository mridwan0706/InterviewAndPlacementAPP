using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T Get(int id);
        Task<int> Post(T entity);
        Task<int> Put(int id, T entity);
        Task<int> Delete(int id);
    }
}
