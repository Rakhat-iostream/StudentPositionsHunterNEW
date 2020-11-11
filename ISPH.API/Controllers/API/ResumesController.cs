using System.IO;
using System.Threading.Tasks;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ISPH.Core.DTO;

namespace ISPH.API.Controllers
{
    [Route("users/students/id={id}/resume/")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumesRepository _repos;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public ResumesController(IResumesRepository repos, IWebHostEnvironment environment, IMapper mapper)
        {
            _repos = repos;
            _env = environment;
            _mapper = mapper;
        }

        

        [HttpPost("add")]
        public async Task<IActionResult> AddResume(IFormFile file, int id)
        {
            if(file != null)
            {
                if (file.ContentType != "application/pdf") return BadRequest(new { message = "Wrong format of resume" });
                string path = "/Resumes/" + file.FileName;
                using(var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                Resume resume = new Resume() { Name = file.FileName, Path = path, StudentId = id };
                if (await _repos.HasEntity(resume)) return BadRequest(new { message = "Your resume already exists" });
                if (await _repos.Create(resume)) return LocalRedirect("/home/profile");
                return BadRequest(new { message = "Failed to upload file" });
            }

            return BadRequest(new { message = "You didn't upload file" });
        }

        [HttpGet]
        public async Task<ResumeDTO> GetResumeByStudentId(int id)
        {
            var resume = await _repos.GetById(id);
            return _mapper.Map<ResumeDTO>(resume);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadResume(int id)
        {
            var resume = await _repos.GetById(id);
            var path = resume.Path;
            FileStreamResult result;
            using(var memoryStream = new MemoryStream())
            {
               using(var stream = new FileStream(_env.WebRootPath + path, FileMode.Open))
               {
                   await stream.CopyToAsync(memoryStream);
               }
                memoryStream.Position = 0;
                result = File(memoryStream, "application/pdf", Path.GetFileName(path));
            }
            return result;
        }



        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteResumeAsync(int id)
        {
            var file = await _repos.GetById(id);
            if (file == null) return BadRequest("This file doesn't exist");
            string fullPath = Path.GetFullPath("static" + file.Path);
            System.IO.File.Delete(fullPath);
            if (await _repos.Delete(file)) return LocalRedirect("/home/profile");
            return BadRequest("Failed to delete resume");
        }
    }
}
