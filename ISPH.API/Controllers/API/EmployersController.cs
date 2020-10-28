using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("users/[controller]/")]
    [Authorize]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployersRepository _repos;
        private readonly IMapper _mapper;
        public EmployersController(IEmployersRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IList<EmployerDTO>> GetAllEmployersAsync()
        {
            var emp = await _repos.GetAll();
            return _mapper.Map<IList<EmployerDTO>>(emp);
        }
        [HttpGet("id={id}")]
        [AllowAnonymous]
        public async Task<Employer> GetEmployerAsync(int id)
        {
            return await _repos.GetById(id);
        }


        [HttpPut("id={id}/update/email")]
        public async Task<IActionResult> UpdateEmployerEmailAsync(EmployerDTO em, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
                Employer employer = await _repos.GetById(id);
                if (await _repos.HasEntity(employer))
                {
                    employer.Email = em.Email;
                    if ( _repos.Update(employer)) return Ok("Updated employer");
                    return BadRequest("Failed to update employer");
                }
            return BadRequest("This employer is not in database");
        }

        [HttpPut("id={id}/update/company")]
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
        public async Task<IActionResult> DeleteEmployerAsync(int id)
        {
            Employer employer = await _repos.GetById(id);
            if (employer == null) return BadRequest("This employer is already deleted");
            if(await _repos.Delete(employer))
            return Ok("Deleted employer");
            return BadRequest("Failed to delete employer");
        }
    }
}
