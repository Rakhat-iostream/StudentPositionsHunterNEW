using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Repositories;
namespace ISPH.Infrastructure.Repositories
{
   public abstract class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        protected readonly EntityContext Context;
        protected EntityRepository(EntityContext context)
        {
            Context = context;
        }
        public virtual async Task<bool> Create(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public abstract Task<bool> HasEntity(T entity);

        public virtual async Task<bool> Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
