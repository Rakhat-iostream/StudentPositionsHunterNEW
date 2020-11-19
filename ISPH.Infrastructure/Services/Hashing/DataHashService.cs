using System.Security.Cryptography;
using System.Text;

namespace ISPH.Infrastructure.Services.Hashing
{
    public abstract class DataHashService<T>
    {
        public void CreateHashedPassword(string password, out byte[] hashedPassword, out byte[] saltPassword)
        {
            using (var hmac = new HMACSHA512())
            {
                saltPassword = hmac.Key;
                hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public abstract bool CheckHashedPassword(T user, string password);

    }

    
}
