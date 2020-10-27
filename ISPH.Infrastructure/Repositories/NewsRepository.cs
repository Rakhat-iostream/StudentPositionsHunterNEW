using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class NewsRepository : EntityRepository<News>, INewsRepository
    {
        public NewsRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<bool> HasEntity(News entity)
        {
            return await _context.News.AnyAsync(News => News.Title == entity.Title);
        }
        public override async Task<IList<News>> GetAll()
        {
          return await _context.News.AsQueryable().OrderBy(news => news.PublishDate).ToListAsync();
        }
    }
}
