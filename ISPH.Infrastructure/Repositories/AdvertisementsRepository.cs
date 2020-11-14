
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class AdvertisementsRepository : EntityRepository<Advertisement>, IAdvertisementsRepository
    {
        public AdvertisementsRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<bool> Create(Advertisement entity)
        {
            _context.Advertisements.Add(entity);
            var position = await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == entity.PositionName);
            position.Amount++;
            return await _context.SaveChangesAsync() > 0;
        }
        public override async Task<bool> HasEntity(Advertisement entity)
        {
            return await _context.Advertisements.AnyAsync(Advertisement => Advertisement.Title == entity.Title);
        }
        public override async Task<bool> Delete(Advertisement entity)
        {
            _context.Advertisements.Remove(entity);
            var position = await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == entity.PositionName);
            position.Amount--;
            return await _context.SaveChangesAsync() > 0;
        }

        public override async Task<IList<Advertisement>> GetAll()
        {
          return await _context.Advertisements.AsQueryable().Include(adv => adv.Employer).
                OrderBy(adv => adv.AdvertisementId).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsAmount(int amount)
        {
            return await _context.Advertisements.AsQueryable().Include(adv => adv.Employer).OrderBy(adv => adv.Title).Take(amount).ToListAsync();
        }

        public override async Task<Advertisement> GetById(int id)
        {
            return await _context.Advertisements.AsNoTracking().Include(adv => adv.Employer).FirstOrDefaultAsync(adv => adv.AdvertisementId == id);
        }





        public async Task<IList<Advertisement>> GetAdvertisementsForEmployer(int employerid)
        {
            return await _context.Advertisements.AsQueryable().Where(adv => adv.EmployerId == employerid).
                 Include(adv => adv.Employer).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsForPosition(int positionId)
        {
            return await _context.Advertisements.AsQueryable().Where(adv => adv.PositionId == positionId).
                Include(adv => adv.Employer).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsForCompany(int companyId)
        {
            return await _context.Advertisements.AsQueryable().
                Include(adv => adv.Employer).Where(adv => adv.Employer.CompanyId == companyId).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetFilteredAdvertisements(string value)
        {
            string sql = string.Format("SELECT a.\"AdvertisementId\", e.\"EmployerId\", a.\"Title\", a.\"PositionId\"," +
                " a.\"Salary\", a.\"Description\", a.\"PositionName\", e.\"CompanyId\", e.\"CompanyName\"" +
               " FROM \"Advertisements\" a INNER JOIN \"Employers\" e ON a.\"EmployerId\" = e.\"EmployerId\" WHERE " +
                "a.\"PositionName\" LIKE '%{0}%' OR e.\"CompanyName\" LIKE '%{0}%' ORDER BY a.\"Title\"", value);
           return await _context.Advertisements.
                FromSqlRaw(sql).Include(adv => adv.Employer).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetFilteredAdvertisements(FilteredAdvertisementDTO ad)
        {
            var builder = new StringBuilder();
            string sql = "SELECT a.\"AdvertisementId\", e.\"EmployerId\", a.\"Title\", a.\"PositionId\"," +
                    " a.\"Salary\", a.\"Description\", a.\"PositionName\", e.\"CompanyId\", e.\"CompanyName\"" +
                   " FROM \"Advertisements\" a INNER JOIN \"Employers\" e ON a.\"EmployerId\" = e.\"EmployerId\" ";

            builder.Append(sql);
            if (ad.HasValue(ad.CompanyName, ad.PositionName, ad.Salary))
            {
                builder.Append("WHERE ");
            }   
            string pos = ad.PositionName, com = ad.CompanyName;
            int sal = ad.Salary.GetValueOrDefault();
            if(!string.IsNullOrEmpty(pos) && !string.IsNullOrEmpty(com) && sal > 0)
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%' AND e.\"CompanyName\" LIKE '%{1}%' AND a.\"Salary\" = {2}", pos, com, sal));
            }
            else if(!string.IsNullOrEmpty(pos) && !string.IsNullOrEmpty(com))
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%' AND e.\"CompanyName\" LIKE '%{1}%'", pos, com));
            }
            else if(!string.IsNullOrEmpty(pos) && sal > 0)
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%' AND a.\"Salary\" = {1}", pos, sal));
            }
            else if(!string.IsNullOrEmpty(com) && sal > 0)
            {
                builder.Append(string.Format("e.\"CompanyName\" LIKE '%{1}%' AND a.\"Salary\" = {2}", pos, com, sal));
            }
            else if (!string.IsNullOrEmpty(pos))
            {
                builder.Append(string.Format("a.\"PositionName\" LIKE '%{0}%'", pos));
            }
            else if (!string.IsNullOrEmpty(com))
            {
                builder.Append(string.Format("e.\"CompanyName\" LIKE '%{0}%'", com));
            }
            else if (sal > 0)
            {
                builder.Append(string.Format("a.\"Salary\" = {0}", pos));
            }

           builder.Append("ORDER BY a.\"Title\"");
            return await _context.Advertisements.FromSqlRaw(builder.ToString()).Include(adv => adv.Employer).ToListAsync();
        }
    }
}
