using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tidskollen.API.Services
{
    public interface ITidskollen<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(int id);
        Task<T> Add(T newEntity);
        Task<T> Update(T Entity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> GetByName(string name);
    }
}
