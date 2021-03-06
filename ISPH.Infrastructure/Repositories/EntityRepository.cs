﻿using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
   public abstract class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        protected readonly EntityContext _context;
        public EntityRepository(EntityContext context)
        {
            _context = context;
        }
        public virtual async Task<bool> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public abstract Task<bool> HasEntity(T entity);

        public virtual bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
