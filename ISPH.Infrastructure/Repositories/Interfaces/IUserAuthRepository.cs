using System.Threading.Tasks;

namespace ISPH.Infrastructure.Repositories.Interfaces
{
    public interface IUserAuthRepository<T>
    {
         Task<T> Register(T user, string password);
         Task<T> Login(string email, string password);
         Task<bool> UserExists(T user);
    }
}
