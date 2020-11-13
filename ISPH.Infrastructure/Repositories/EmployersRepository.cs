using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using ISPH.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Authentification;

namespace ISPH.Infrastructure.Repositories
{
    public class EmployersRepository : EntityRepository<Employer>, IUserAuthRepository<Employer>, IEmployersRepository
    {
        private readonly DataHashService<Employer> hashService = new EmployersHashService();
        public EmployersRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IList<Employer>> GetAll()
        {
           return await _context.Employers.AsQueryable().Include(emp => emp.Company).Include(emp => emp.Advertisements).
                OrderBy(emp => emp.EmployerId).ToListAsync();
        }
        public override async Task<Employer> GetById(int id)
        {
            return await _context.Employers.Include(emp => emp.Advertisements).FirstOrDefaultAsync(adv => adv.EmployerId == id);
        }
        public override async Task<bool> HasEntity(Employer entity)
        {
            return await _context.Employers.AnyAsync(company => company.Email == entity.Email);
        }

        public async Task<bool> UpdatePassword(Employer entity, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            entity.HashedPassword = hashedPass;
            entity.SaltPassword = SaltPass;
            _context.Employers.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCompany(Employer entity, string companyName)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(com => com.Name == companyName);
            if (company != null)
            {
                entity.CompanyName = companyName;
                entity.CompanyId = company.CompanyId;
                _context.Employers.Update(entity);
                var ads = _context.Advertisements.AsQueryable().Where(ad => ad.EmployerId == entity.EmployerId);
                _context.Advertisements.RemoveRange(ads);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        //Auth

        public async Task<Employer> Register(Employer user, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = SaltPass;
            _context.Employers.Add(user);
            await _context.SaveChangesAsync();
            return user;
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
