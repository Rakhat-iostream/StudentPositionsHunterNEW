using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ISPH.Infrastructure.Services
{
    public static class TokenCreatingService
    {
        public static string CreateToken(ClaimsIdentity identity, out string identityName, IConfiguration Configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            var token = new JwtSecurityToken(
                claims: identity.Claims,
                audience: AuthentificationOptions.AUDIENCE,
                issuer: AuthentificationOptions.ISSUER,
                expires: DateTime.Now.AddHours(AuthentificationOptions.LIFETIME),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );
            var handler = new JwtSecurityTokenHandler();
            var encodedToken = handler.WriteToken(token);
            identityName = identity.Name;
            return encodedToken;
        }

    }
}
