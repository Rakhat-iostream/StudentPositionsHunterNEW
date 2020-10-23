using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
    public interface IEmployersRepository : IEntityRepository<Employer>
    {
        Task<bool> UpdatePassword(Employer employer, string password);
        Task<bool> UpdateCompany(Employer employer, string companyName);
    }
}
