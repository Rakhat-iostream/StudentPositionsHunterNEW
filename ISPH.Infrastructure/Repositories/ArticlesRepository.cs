using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class ArticlesRepository : EntityRepository<Article>, IArticlesRepository
    {
        public ArticlesRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<bool> HasEntity(Article entity)
        {
            return await Context.Articles.AnyAsync(art => art.Title == entity.Title);
        }
        public override async Task<IList<Article>> GetAll()
        {
           return await Context.Articles.AsQueryable().OrderByDescending(art => art.PublishDate).ToListAsync();
        }
    }
}
