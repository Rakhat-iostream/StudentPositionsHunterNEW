using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
         Task<bool> Create(T entity);
         bool Update(T entity);
         Task<bool> Delete(T entity);
         Task<bool> HasEntity(T entity);
         Task<T> GetById(int id);
         Task<IList<T>> GetAll();
    }
}
