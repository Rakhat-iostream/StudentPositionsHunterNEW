using ISPH.Core.Models;
using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Repositories
{
    public interface ICompanyRepository : IEntityRepository<Company>
    {
         Task<Company> GetCompanyByName(string name);
    }
}
