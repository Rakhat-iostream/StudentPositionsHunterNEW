using ISPH.Core.Models;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
         Task<Company> GetCompanyByName(string name);
    }
}
