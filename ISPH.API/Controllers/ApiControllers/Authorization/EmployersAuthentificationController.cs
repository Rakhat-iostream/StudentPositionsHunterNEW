using ISPH.Core.DTO;
using ISPH.Core.DTO.Authorization;
using ISPH.Core.Interfaces.Authentification;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Services.TokenConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ISPH.API.Controllers.ApiControllers.Authorization
{
    [Route("users/employers/auth/")]
    [ApiController]
    public class EmployersAuthentificationController : ControllerBase
    {
        private readonly IUserAuthRepository<Employer> _authRepos;
        private readonly ICompanyRepository _companyRepos;
        private readonly TokenCreatingService<Employer> tokenService;
        private IConfiguration Configuration { get; }
        public EmployersAuthentificationController(IUserAuthRepository<Employer> authRepos, IConfiguration config, ICompanyRepository companyRepos)
        {
            _authRepos = authRepos;
            Configuration = config;
            _companyRepos = companyRepos;
            tokenService = new EmployerTokenService(_authRepos);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployer(EmployerDto em)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields" });
            var employer = new Employer()
            {
                FirstName = em.FirstName,
                LastName = em.LastName,
                Email = em.Email,
                Role = "employer",
                CompanyName = em.CompanyName
            };
            var company = await _companyRepos.GetCompanyByName(em.CompanyName);
            if (company == null) return BadRequest(new { message = "Such company doesn't exist" });
            if (await _authRepos.UserExists(employer)) return BadRequest(new { message = "This user already exists" });
            employer.CompanyId = company.CompanyId;
            employer.CompanyName = company.Name;
            employer = await _authRepos.Register(employer, em.Password);
            if (employer == null) return BadRequest(new { message = "Failed to register" });
            var identity = await tokenService.CreateIdentity(em.Email, em.Password);
            string token = tokenService.CreateToken(identity, out string identityName, Configuration);

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", employer.EmployerId);
            HttpContext.Session.SetString("Name", employer.FirstName);
            HttpContext.Session.SetString("Company", employer.CompanyName);
            HttpContext.Session.SetString("Role", employer.Role);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginEmployer(LoginDto em)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields" });
            var employer = await tokenService.CreateIdentity(em.Email, em.Password);
            if (employer == null) return Unauthorized(new { message = "Username or password is incorrect" });
            string token = tokenService.CreateToken(employer, out string identityName, Configuration);
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", int.Parse(employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));
            HttpContext.Session.SetString("Name", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value);
            HttpContext.Session.SetString("Company", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.UserData).Value);
            HttpContext.Session.SetString("Role", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("signout")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return LocalRedirect("~/home/index");
        }

    }
}
