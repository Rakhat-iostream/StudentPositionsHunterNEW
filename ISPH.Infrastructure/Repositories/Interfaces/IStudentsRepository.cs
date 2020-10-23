using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
   public interface IStudentsRepository : IEntityRepository<Student>
    {
        Task<bool> UpdatePassword(Student entity, string password);
    }
}
