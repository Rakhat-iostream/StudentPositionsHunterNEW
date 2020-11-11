
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var adsPos = _context.Advertisements.Where(adv => adv.PositionName == value);
            var adsCom = _context.Advertisements.Where(adv => adv.Employer.CompanyName == value);
            return await adsPos.Union(adsCom).Include(adv => adv.Employer).Include(adv => adv.Position).ToListAsync();
        }
    }
}
