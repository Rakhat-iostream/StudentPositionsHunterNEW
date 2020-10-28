using ISPH.Core.Models;
using System.Threading.Tasks;
using ISPH.Core.Interfaces.Repositories;
namespace ISPH.Core.Interfaces.Repositories
{
   public interface IStudentsRepository : IEntityRepository<Student>
    {
        Task<bool> UpdatePassword(Student entity, string password);
    }
}
