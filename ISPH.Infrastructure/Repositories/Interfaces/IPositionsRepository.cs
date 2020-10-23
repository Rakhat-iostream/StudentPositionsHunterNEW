using ISPH.Core.Models;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
    public interface IPositionsRepository : IEntityRepository<Position>
    {
         Task<Position> GetPositionByName(string name);
    }
    
}
