using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Authorize]
    [Route("users/[controller]/")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _repos;
        private readonly IMapper _mapper;
        public StudentsController(IStudentsRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IList<StudentDTO>> GetAllStudents()
        {
            var sts = await _repos.GetAll();
            return _mapper.Map<IList<StudentDTO>>(sts);
        }
        [HttpGet("id={id}")]
        public async Task<Student> GetStudentAsync(int id)
        {
            return await _repos.GetById(id);
        }
        

        [HttpPut("id={id}/update/email")]
        public async Task<IActionResult> UpdateStudentEmailAsync(StudentDTO st, int id)
        {
            if(!ModelState.IsValid) return BadRequest("Fill all fields");
            Student student = await _repos.GetById(id);
                if (student != null)
                {
                    student.Email = st.Email;
                        if ( _repos.Update(student)) return Ok("Updated student");
                        
                    return BadRequest("Failed to update student");
                }
                return BadRequest("This student doesn't exist");
        }

        [HttpPut("id={id}/update/password")]
        public async Task<IActionResult> UpdateStudentPasswordAsync(StudentDTO st, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Student student = await _repos.GetById(id);
            if (await _repos.HasEntity(student))
            {
                if (await _repos.UpdatePassword(student, st.Password)) return Ok("Updated");
                return BadRequest("Failed to update employer");
            }
            return BadRequest("This employer is not in database");
        }


        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            Student student = await _repos.GetById(id);
            if (student == null) return BadRequest("This student is already deleted");
            if (await _repos.Delete(student))
            {
                return Ok("Deleted student");
            }
            return BadRequest("Failed to delete student");
        }
    }
}
