using ISPH.Core.Models;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Repositories;
namespace ISPH.Core.Interfaces.Repositories
{
    public interface IPositionsRepository : IEntityRepository<Position>
    {
         Task<Position> GetPositionByName(string name);
    }
    
}
