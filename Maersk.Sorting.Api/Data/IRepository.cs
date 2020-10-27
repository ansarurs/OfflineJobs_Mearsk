using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Maersk.Sorting.Api.Data
{
    public interface IRepository<T> 
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
     
        Task<T> Get(Guid jobId);
       
    }
}
