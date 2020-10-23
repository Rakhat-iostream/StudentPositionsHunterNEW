using System.IO;
using System.Threading.Tasks;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("users/students/id={id}/resume/")]
    [Authorize]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumesRepository _repos;
        private readonly IWebHostEnvironment _env;
        public ResumesController(IResumesRepository repos, IWebHostEnvironment environment)
        {
            _repos = repos;
            _env = environment;
        }

        [HttpGet]
        public async Task<Resume> GetResumeByStudentIdAsync(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddResume(IFormFile file)
        {
            if(file != null)
            {
                string path = "/Resumes/" + file.FileName;
                using(var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                Resume resume = new Resume() { Name = file.FileName, Path = path };
                if (await _repos.Create(resume)) return Ok("Uploaded new file");
                return BadRequest("Failed to upload file");
            }

            return BadRequest("You didn't upload file");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteResumeAsync(int id)
        {
            if (await _repos.Delete(await _repos.GetById(id))) return Ok("Deleted resume");
            return BadRequest("Failed to delete resume");
        }
    }
}
