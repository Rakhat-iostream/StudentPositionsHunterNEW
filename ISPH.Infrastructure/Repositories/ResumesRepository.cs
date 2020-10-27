using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class ResumesRepository : EntityRepository<Resume>, IResumesRepository
    {
        public ResumesRepository(EntityContext context) : base(context)
        {
        }
        public async override Task<Resume> GetById(int id)
        {
            return await _context.Resumes.FirstOrDefaultAsync(res => res.StudentId == id);
        }

        public override async Task<bool> HasEntity(Resume resume)
        {
            return await _context.Resumes.AnyAsync(res => res.StudentId == resume.StudentId);
        }

        public override async Task<IList<Resume>> GetAll()
        {
            return await _context.Resumes.AsQueryable().Include(res => res.Student).ToListAsync();
        }
    }
}
