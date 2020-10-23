using ISPH.Core.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Interfaces;
using ISPH.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class EmployersRepository : IEntityRepository<Employer>, IUserAuthRepository<Employer>
    {
        private readonly EntityContext _context;
        private readonly DataHashService<Employer> hashService = new EmployersHashService();
        public EmployersRepository(EntityContext context)
        {
            _context = context;
        }

        public async Task<IList<Employer>> GetAll()
        {
           return await _context.Employers.AsQueryable().Include(emp => emp.Company).Include(emp => emp.Advertisements).
                OrderBy(emp => emp.EmployerId).ToListAsync();
        }
        public async Task<Employer> GetById(int id)
        {
            var employer = await _context.Employers.FindAsync(id);
            employer.Advertisements = await _context.Advertisements.AsQueryable().Where(adv => adv.EmployerId == id).ToListAsync();
            return employer;
        }
        public async Task<bool> Create(Employer entity)
        {
            _context.Employers.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> HasEntity(Employer entity)
        {
            return await _context.Employers.AnyAsync(company => company.Email == entity.Email);
        }
        public async Task<bool> Delete(Employer entity)
        {
            _context.Employers.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Update(Employer entity)
        {
            Employer employer = _context.Employers.FirstOrDefault(emp => emp.EmployerId == entity.EmployerId);
            if(employer.CompanyName != entity.CompanyName)
            {
                Company company = _context.Companies.FirstOrDefault(comp => comp.Name == entity.CompanyName);
                employer.CompanyName = entity.CompanyName;
                employer.CompanyId = company.CompanyId;
                employer.Company = company;
                var ads = _context.Advertisements.Where(adv => adv.EmployerId == employer.EmployerId).ToList();
                _context.Advertisements.RemoveRange(ads);
            }
            _context.Employers.Update(employer);
            return _context.SaveChanges() > 0;
        }
        public async Task<bool> UpdatePassword(Employer entity, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            entity.HashedPassword = hashedPass;
            entity.SaltPassword = SaltPass;
            _context.Employers.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        //Auth

        public async Task<Employer> Register(Employer user, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = SaltPass;
            if (await Create(user)) return await _context.Employers.FirstOrDefaultAsync(emp => emp.Email == user.Email);
            return null;
        }

        public async Task<Employer> Login(string email, string password)
        {
            var user = await _context.Employers.FirstOrDefaultAsync(em => em.Email == email);
            if (user != null)
            {
                if (hashService.CheckHashedPassword(user, password)) return user;
            }
            return null;
        }

        public async Task<bool> UserExists(Employer user)
        {
            return await _context.Employers.AnyAsync(em => em.Email == user.Email);
        }
    }
}
