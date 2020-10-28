using System.Threading.Tasks;

namespace ISPH.Core.Interfaces.Authentification
{
    public interface IUserAuthRepository<T>
    {
         Task<T> Register(T user, string password);
         Task<T> Login(string email, string password);
         Task<bool> UserExists(T user);
    }
}
