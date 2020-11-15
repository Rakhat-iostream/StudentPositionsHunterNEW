using ISPH.Core.DTO;
using ISPH.Core.Enums;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IAdvertisementsRepository : IEntityRepository<Advertisement>
    {
        Task<IList<Advertisement>> GetAdvertisementsByEntityId(int id, EntityType type);
        Task<IList<Advertisement>> GetFilteredAdvertisements(string value);
        Task<IList<Advertisement>> GetFilteredAdvertisements(FilteredAdvertisementDto ad);
        Task<int> GetAdvertisementsCount();

        Task<IList<Advertisement>> GetAdvertisementsPerPage(int page);
        Task<IList<Advertisement>> GetAdvertisementsAmount(int amount);
    }
}
