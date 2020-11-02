using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly EntityContext _context;
        public FavouritesRepository(EntityContext context)
        {
            _context = context;
        }

        public async Task<FavouriteAdvertisement> GetById(int studentId, int adId)
        {
            return await _context.Favourites.FirstOrDefaultAsync(fav => fav.AdvertisementId == adId && fav.StudentId == studentId);
        }
        public async Task<bool> AddToFavourites(FavouriteAdvertisement ad)
        {
            _context.Favourites.Add(ad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFromFavourites(FavouriteAdvertisement ad)
        {
            _context.Favourites.Remove(ad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<FavouriteAdvertisement>> GetFavourites(int id)
        {
            return await _context.Favourites.AsQueryable().Where(fav => fav.StudentId == id).Include(fav => fav.Advertisement).ToListAsync();
        }
    }
}
