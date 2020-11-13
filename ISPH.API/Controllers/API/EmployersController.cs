using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISPH.Infrastructure;
using ISPH.Infrastructure.Configuration;

namespace ISPH.API.Controllers
{
    [Route("users/[controller]/")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployersRepository _repos;
        public EmployersController(IEmployersRepository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IList<Employer>> GetAllEmployersAsync()
        {
            return await _repos.GetAll();
        }
        [HttpGet("id={id}")]
        public async Task<Employer> GetEmployerAsync(int id)
        {
            return await _repos.GetById(id);
        }


        [HttpPut("id={id}/update/email")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerEmailAsync(EmployerDTO em, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
                Employer employer = await _repos.GetById(id);
                if (await _repos.HasEntity(employer))
                {
                    employer.Email = em.Email;
                    if (await _repos.Update(employer)) return Ok("Updated employer");
                    return BadRequest("Failed to update employer");
                }
            return BadRequest("This employer is not in database");
        }

        [HttpPut("id={id}/update/company")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerCompanyAsync(EmployerDTO em, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Employer employer = await _repos.GetById(id);
            if (await _repos.HasEntity(employer))
            {
                if (await _repos.UpdateCompany(employer, em.CompanyName))
                {
                    return Ok("Updated employer");
                }
                return BadRequest("Failed to update employer");
            }
            return BadRequest("This employer is not in database");
        }

        [HttpPut("id={id}/update/password")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateEmployerPasswordAsync(EmployerDTO st, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Employer employer = await _repos.GetById(id);
            if (await _repos.HasEntity(employer))
            {
                if (await _repos.UpdatePassword(employer, st.Password)) return Ok(new { message = "Updated"});
                return BadRequest("Failed to update employer");
            }
            return BadRequest("This employer is not in database");
        }


        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteEmployerAsync(int id)
        {
            Employer employer = await _repos.GetById(id);
            if (employer == null) return BadRequest("This employer is already deleted");
            if(await _repos.Delete(employer)) return Ok("Deleted employer");
            return BadRequest("Failed to delete employer");
        }
    }
}
