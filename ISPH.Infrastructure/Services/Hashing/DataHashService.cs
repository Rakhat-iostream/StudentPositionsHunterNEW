using System.Security.Cryptography;
using System.Text;

namespace ISPH.Infrastructure.Repositories.Services.Hashing
{
    public abstract class DataHashService<T>
    {
        public void CreateHashedPassword(string password, out byte[] hashedPassword, out byte[] SaltPassword)
        {
            using (var hmac = new HMACSHA512())
            {
                SaltPassword = hmac.Key;
                hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public abstract bool CheckHashedPassword(T user, string password);

    }

    
}
