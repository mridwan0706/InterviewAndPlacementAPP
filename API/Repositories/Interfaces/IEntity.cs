using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
   public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        void Create();
        void Update();
        void Delete();
    }
}
