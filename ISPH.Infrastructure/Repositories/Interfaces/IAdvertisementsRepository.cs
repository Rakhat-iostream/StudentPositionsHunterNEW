using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
    public interface IAdvertisementsRepository
    {
        Task<IList<Advertisement>> GetAdvertisementsForPosition(int positionId);
        Task<IList<Advertisement>> GetAdvertisementsForEmployer(int employerId);
        Task<IList<Advertisement>> GetAdvertisementsForCompany(int companyId);
    }
}
