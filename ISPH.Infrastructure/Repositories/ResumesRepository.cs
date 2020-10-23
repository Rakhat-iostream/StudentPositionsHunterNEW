using ISPH.Core.Data;
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
        /*public async Task<Resume> GetById(int StudentId)
        {
            return await _context.Resumes.FindAsync(StudentId);
        }

        public async Task<bool> Create(Resume resume)
        {
            _context.Resumes.Add(resume);
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Update(Resume resume)
        {
            _context.Resumes.Update(resume);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> Delete(Resume resume)
        {
            _context.Resumes.Remove(resume);
            return await _context.SaveChangesAsync() > 0;
        }*/

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
