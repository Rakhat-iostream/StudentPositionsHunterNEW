using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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
       /* public async Task<bool> Create(Company entity)
        {
            _context.Companies.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Company entity)
        {
            _context.Companies.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }*/

        public override async Task<IList<Company>> GetAll()
        {
            var companies = await _context.Companies.AsQueryable().Include(company => company.Employers).
               OrderBy(company => company.CompanyId).ToListAsync();
            return companies;
        }

        public override async Task<Company> GetById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            company.Employers = await _context.Employers.AsQueryable().Where(emp => emp.CompanyName == company.Name).ToListAsync();
            return company;
        }
       
        public override async Task<bool> HasEntity(Company entity)
        {
            return await _context.Companies.AnyAsync(company => company.Name == entity.Name);
        }

        /*public bool Update(Company entity)
        {
            _context.Companies.Update(entity);
            return _context.SaveChanges() > 0;
        }
        */
        public async Task<Company> GetCompanyByName(string name)
        {
            return await _context.Companies.FirstOrDefaultAsync(company => company.Name == name);
        }
    }
}
