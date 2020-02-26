using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface ISiteRepository
    {
        IEnumerable<Site> Get();
        Site Get(int id);
        Task<int> Post(Site site);
        Task<int> Put(int id, Site site);
        Task<int> Delete(int id);
    }
}
