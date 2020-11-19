using ISPH.Core.Interfaces.Authentification;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Services.TokenConfiguration
{
  public  class StudentTokenService : TokenCreatingService<Student>
    {
        public StudentTokenService(IUserAuthRepository<Student> repos) : base(repos)
        {
        }
        public override async Task<ClaimsIdentity> CreateIdentity(string email, string password)
        {
            var student = await _repos.Login(email, password);
            if (student != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
                    new Claim(ClaimTypes.Name, student.FirstName),
                    new Claim(ClaimTypes.Email, student.Email),
                    new Claim(ClaimTypes.Role, student.Role),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                return claimsIdentity;
            }
            return null;
        }
    }
}
