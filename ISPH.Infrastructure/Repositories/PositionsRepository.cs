using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class PositionsRepository : EntityRepository<Position>, IPositionsRepository
    {
        public PositionsRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<IList<Position>> GetAll()
        {
           return await _context.Positions.AsQueryable().Include(pos => pos.Advertisements).ToListAsync();
        }

        public override async Task<Position> GetById(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            position.Advertisements = await _context.Advertisements.Where(ads => ads.PositionId == position.PositionId).ToListAsync();
            return position;
        }

        public async Task<Position> GetPositionByName(string name)
        {
            var position = await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == name);
            position.Advertisements = await _context.Advertisements.Where(ads => ads.PositionId == position.PositionId).ToListAsync();
            return position;
        }

        public override async Task<bool> HasEntity(Position entity)
        {
            return await _context.Positions.AnyAsync(pos => pos.Name == entity.Name);
        }
        
    }
}
