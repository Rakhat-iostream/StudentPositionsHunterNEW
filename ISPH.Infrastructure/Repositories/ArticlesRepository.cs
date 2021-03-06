﻿
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using ISPH.Infrastructure.Repositories.Interfaces;
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
            return await _context.Articles.AnyAsync(Article => Article.Title == entity.Title);
        }
        public override async Task<IList<Article>> GetAll()
        {
           return await _context.Articles.AsQueryable().OrderBy(article => article.PublishDate).ToListAsync();
        }

        public override async Task<Article> GetById(int id)
        {
            return await _context.Articles.FindAsync(id);
        }
    }
}
