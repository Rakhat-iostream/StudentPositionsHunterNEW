using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class CompaniesRepository : EntityRepository<Company>, ICompanyRepository
    {
        public CompaniesRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IList<Company>> GetAll()
        {
            return await _context.Companies.AsQueryable().Include(company => company.Employers).
                OrderBy(company => company.CompanyId)
               .ToListAsync();
        }
       
        public override async Task<bool> HasEntity(Company entity)
        {
            return await _context.Companies.AnyAsync(company => company.Name == entity.Name);
        }

        public async Task<Company> GetCompanyByName(string name)
        {
            return await _context.Companies.FirstOrDefaultAsync(company => company.Name == name);
        }
    }
}
