using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface IAdvertisementsRepository : IEntityRepository<Advertisement>
    {
        Task<IList<Advertisement>> GetAdvertisementsForPosition(int positionId);
        Task<IList<Advertisement>> GetAdvertisementsForEmployer(int employerId);
        Task<IList<Advertisement>> GetAdvertisementsForCompany(int companyId);
        Task<IList<Advertisement>> GetFilteredAdvertisements(string value);

        Task<IList<Advertisement>> GetAdvertisementsAmount(int amount);
    }
}
