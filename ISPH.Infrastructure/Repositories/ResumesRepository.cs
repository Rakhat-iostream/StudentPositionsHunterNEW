﻿using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ISPH.Infrastructure.Repositories
{
    public class ResumesRepository : EntityRepository<Resume>, IResumesRepository
    {
        public ResumesRepository(EntityContext context) : base(context)
        {
        }
        public async override Task<Resume> GetById(int id)
        {
            return await _context.Resumes.AsNoTracking().FirstOrDefaultAsync(res => res.StudentId == id);
        }

        public override async Task<bool> HasEntity(Resume resume)
        {
            return await _context.Resumes.AnyAsync(res => res.StudentId == resume.StudentId);
        }

        public override async Task<IList<Resume>> GetAll()
        {
            return await _context.Resumes.AsQueryable().OrderBy(res => res.Id).Include(res => res.Student).ToListAsync();
        }
    }
}
