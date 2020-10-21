using ISPH.Core.Models;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
    public interface IPositionsRepository
    {
         Task<Position> GetPositionByName(string name);
         Task<Position> GetPositionById(int id);
    }
    
}
