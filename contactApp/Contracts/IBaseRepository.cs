using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contactApp.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(int id);

        IQueryable<T> Query();

        Task InsertAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task Commit();
    }
}