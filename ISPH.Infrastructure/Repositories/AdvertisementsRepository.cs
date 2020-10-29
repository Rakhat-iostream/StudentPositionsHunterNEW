
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
            _context.Positions.Update(position);
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
            _context.Positions.Update(position);
            return await _context.SaveChangesAsync() > 0;
        }

        public override async Task<IList<Advertisement>> GetAll()
        {
          return await _context.Advertisements.AsQueryable().Include(adv => adv.Employer).
                OrderBy(adv => adv.AdvertisementId).ToListAsync();
        }

        public override async Task<Advertisement> GetById(int id)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            ad.Employer = await _context.Employers.FirstOrDefaultAsync(emp => emp.EmployerId == ad.EmployerId);
            return ad;
        }


        public async Task<IList<Advertisement>> GetAdvertisementsForEmployer(int employerid)
        {
            return await _context.Advertisements.AsQueryable()
                 .Include(adv => adv.Employer).Where(adv => adv.EmployerId == employerid).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsForPosition(int positionId)
        {
            return await _context.Advertisements.AsQueryable().Include(adv => adv.Employer).Where(adv => adv.PositionId == positionId).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsForCompany(int companyId)
        {
            return await _context.Advertisements.AsQueryable().Include(adv => adv.Employer).Where(adv => adv.Employer.CompanyId == companyId).ToListAsync();
        }
    }
}
