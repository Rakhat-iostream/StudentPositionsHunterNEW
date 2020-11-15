using ISPH.Core.DTO;
using ISPH.Core.DTO.Authorization;
using ISPH.Core.Interfaces.Authentification;
using ISPH.Core.Models;
using ISPH.Infrastructure.Services.TokenConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ISPH.API.Controllers.ApiControllers.Authorization 
{
    [Route("users/students/auth/")]
    [ApiController]
    public class StudentsAuthentificationController : ControllerBase
    {
        private readonly IUserAuthRepository<Student> _authRepos;
        private readonly TokenCreatingService<Student> tokenService;
        private IConfiguration Configuration { get; }
        public StudentsAuthentificationController(IUserAuthRepository<Student> authRepos, IConfiguration config)
        {
            _authRepos = authRepos;
            Configuration = config;
            tokenService = new StudentTokenService(_authRepos);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(StudentDto st)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields"});
            var student = new Student()
            {
                FirstName = st.FirstName,
                LastName = st.LastName,
                Email = st.Email,
                Role = "student",
            };
            if (await _authRepos.UserExists(student)) return BadRequest(new { message = "This user already exists" });
            student = await _authRepos.Register(student, st.Password);
            if (student == null) return BadRequest(new { message = "Oops, failed to register" });
            var identity = await tokenService.CreateIdentity(st.Email, st.Password);
            string token = tokenService.CreateToken(identity, out string identityName, Configuration);
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", student.StudentId);
            HttpContext.Session.SetString("Name", student.FirstName);
            HttpContext.Session.SetString("Role", student.Role);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginStudent(LoginDto st)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields" });
            var student = await tokenService.CreateIdentity(st.Email, st.Password);
            if (student == null) return Unauthorized(new { message = "Username or password is incorrect" });
            string token = tokenService.CreateToken(student, out string identityName, Configuration);
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", int.Parse(student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));
            HttpContext.Session.SetString("Name", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value);
            HttpContext.Session.SetString("Role", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value);
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
