using ISPH.Core.DTO;
using ISPH.Core.Enums;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class AdvertisementsRepository : EntityRepository<Advertisement>, IAdvertisementsRepository
    {
        private readonly IDictionary<EntityType, FilteredAds> _filteredMap;
        private delegate Task<IList<Advertisement>> FilteredAds(int id);
        public AdvertisementsRepository(EntityContext context) : base(context)
        {
            _filteredMap = new Dictionary<EntityType, FilteredAds>();
            if (!_filteredMap.ContainsKey(EntityType.Company)) _filteredMap.Add(EntityType.Company, GetAdvertisementsForCompany);
            if (!_filteredMap.ContainsKey(EntityType.Employer)) _filteredMap.Add(EntityType.Employer, GetAdvertisementsForEmployer);
            if (!_filteredMap.ContainsKey(EntityType.Position)) _filteredMap.Add(EntityType.Position, GetAdvertisementsForPosition);
        }
        public override async Task<bool> Create(Advertisement entity)
        {
            await Context.Advertisements.AddAsync(entity);
            var position = await Context.Positions.FirstOrDefaultAsync(pos => pos.Name == entity.PositionName);
            position.Amount++;
            return await Context.SaveChangesAsync() > 0;
        }
        public override async Task<bool> HasEntity(Advertisement entity)
        {
            return await Context.Advertisements.AnyAsync(adv => adv.Title == entity.Title);
        }
        public override async Task<bool> Delete(Advertisement entity)
        {
            Context.Advertisements.Remove(entity);
            var position = await Context.Positions.FirstOrDefaultAsync(pos => pos.Name == entity.PositionName);
            position.Amount--;
            return await Context.SaveChangesAsync() > 0;
        }

        public override async Task<IList<Advertisement>> GetAll()
        {
            return await Context.Advertisements.AsQueryable().OrderBy(adv => adv.Title).
                Include(adv => adv.Employer).ToListAsync(); 
        }

        public async Task<IList<Advertisement>> GetAdvertisementsAmount(int amount)
        {
            return await Context.Advertisements.AsQueryable().OrderBy(adv => adv.Title).
                Include(adv => adv.Employer).
                Take(amount).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsPerPage(int page)
        {
            return await Context.Advertisements.AsQueryable().OrderBy(adv => adv.Title).
                Skip((page - 1) * 4).Take(4).
                Include(adv => adv.Employer).
                ToListAsync();
        }

        public override async Task<Advertisement> GetById(int id)
        {
            return await Context.Advertisements.AsNoTracking().Include(adv => adv.Employer).
                FirstOrDefaultAsync(adv => adv.AdvertisementId == id);
        }

        public async Task<IList<Advertisement>> GetAdvertisementsByEntityId(int id, EntityType type)
        {
           return await _filteredMap[type].Invoke(id);
        }



        //Ads for specific model id
        private async Task<IList<Advertisement>> GetAdvertisementsForEmployer(int employerid)
        {
            return await Context.Advertisements.AsQueryable().Where(adv => adv.EmployerId == employerid)
                .Include(adv => adv.Employer).ToListAsync();
        }

        private async Task<IList<Advertisement>> GetAdvertisementsForPosition(int positionId)
        {
            return await Context.Advertisements.AsQueryable().Where(adv => adv.PositionId == positionId)
                .Include(adv => adv.Employer).ToListAsync(); 
        }

        private async Task<IList<Advertisement>> GetAdvertisementsForCompany(int companyId)
        {
            return await Context.Advertisements.AsQueryable().
                Where(adv => adv.Employer.CompanyId == companyId).Include(adv => adv.Employer).
                ToListAsync();
        }


        //filtering
        public async Task<IList<Advertisement>> GetFilteredAdvertisements(string value)
        {
            string sql = string.Format("SELECT a.\"AdvertisementId\", e.\"EmployerId\", a.\"Title\", a.\"PositionId\"," +
                " a.\"Salary\", a.\"Description\", a.\"PositionName\", e.\"CompanyId\", e.\"CompanyName\"" +
               " FROM \"Advertisements\" a INNER JOIN \"Employers\" e ON a.\"EmployerId\" = e.\"EmployerId\" WHERE " +
                "a.\"PositionName\" LIKE '%{0}%' OR e.\"CompanyName\" LIKE '%{0}%' ORDER BY a.\"Title\"", value);
            return await Context.Advertisements.FromSqlRaw(sql).Include(adv => adv.Employer).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetFilteredAdvertisements(FilteredAdvertisementDto ad)
        {
            var builder = new StringBuilder();
            string sql = "SELECT a.\"AdvertisementId\", e.\"EmployerId\", a.\"Title\", a.\"PositionId\"," +
                    " a.\"Salary\", a.\"Description\", a.\"PositionName\", e.\"CompanyId\", e.\"CompanyName\" " +
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
                builder.Append(string.Format("e.\"CompanyName\" LIKE '%{0}%' AND a.\"Salary\" = {1}", com, sal));
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
                builder.Append(string.Format("a.\"Salary\" = {0}", sal));
            }
           builder.Append("ORDER BY a.\"Title\"");
            return await Context.Advertisements.FromSqlRaw(builder.ToString()).Include(adv => adv.Employer).
                ToListAsync();
        }

        public async Task<int> GetAdvertisementsCount()
        {
            return await Context.Advertisements.CountAsync();
        }
    }
}
