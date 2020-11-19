using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using Microsoft.EntityFrameworkCore;
using ISPH.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Authentification;
using ISPH.Infrastructure.Services.Hashing;

namespace ISPH.Infrastructure.Repositories
{
    public class EmployersRepository : EntityRepository<Employer>, IUserAuthRepository<Employer>, IEmployersRepository
    {
        private readonly DataHashService<Employer> _hashService = new EmployersHashService();
        public EmployersRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IList<Employer>> GetAll()
        {
           return await Context.Employers.AsQueryable().OrderBy(emp => emp.EmployerId).
               Include(emp => emp.Company).Include(emp => emp.Advertisements).
                ToListAsync();
        }
        public override async Task<Employer> GetById(int id)
        {
            return await Context.Employers.AsNoTracking().Include(emp => emp.Advertisements).
                FirstOrDefaultAsync(adv => adv.EmployerId == id);
        }
        public override async Task<bool> HasEntity(Employer entity)
        {
            return await Context.Employers.AnyAsync(company => company.Email == entity.Email);
        }

        public async Task<bool> UpdatePassword(Employer entity, string password)
        {
            _hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] saltPass);
            entity.HashedPassword = hashedPass;
            entity.SaltPassword = saltPass;
            Context.Employers.Update(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCompany(Employer entity, string companyName)
        {
            var company = await Context.Companies.FirstOrDefaultAsync(com => com.Name == companyName);
            if (company != null)
            {
                entity.CompanyName = companyName;
                entity.CompanyId = company.CompanyId;
                Context.Employers.Update(entity);
                var ads = Context.Advertisements.AsQueryable().
                    Where(ad => ad.EmployerId == entity.EmployerId);
                Context.Advertisements.RemoveRange(ads);
            }
            return await Context.SaveChangesAsync() > 0;
        }
        //Auth

        public async Task<Employer> Register(Employer user, string password)
        {
            _hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] saltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = saltPass;
            await Context.Employers.AddAsync(user);
            await Context.SaveChangesAsync();
            return user;
        }

        public async Task<Employer> Login(string email, string password)
        {
            var user = await Context.Employers.FirstOrDefaultAsync(em => em.Email == email);
            if (user != null)
            {
                if (_hashService.CheckHashedPassword(user, password)) return user;
            }
            return null;
        }

        public async Task<bool> UserExists(Employer user)
        {
            return await Context.Employers.AnyAsync(em => em.Email == user.Email);
        }
    }
}
