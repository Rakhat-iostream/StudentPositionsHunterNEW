using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure;
using ISPH.Infrastructure.Repositories;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repos;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;   
        public NewsController(INewsRepository repos, IWebHostEnvironment env, IMapper mapper)
        {
            _repos = repos;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<NewsDTO>> GetAllNewsAsync()
        {
            var news = await _repos.GetAll();
            return _mapper.Map<IList<NewsDTO>>(news);
        }

        [HttpGet("id={id}")]
        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
       // [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> AddNewsAsync([FromForm] NewsDTO newsDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            string path = "/images/" + newsDTO.File.FileName;
            News news = new News()
            {
                Title = newsDTO.Title,
                PublishDate = newsDTO.PublishDate.GetValueOrDefault(),
                Description = newsDTO.Description,
                ImagePath = path
            };
            if (await _repos.HasEntity(news)) return BadRequest("These news already exist");
            using (var stream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
               await newsDTO.File.CopyToAsync(stream);
            }
            if (await _repos.Create(news)) return LocalRedirect("/home/index");

            return BadRequest("Failed to add news");
        }

        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateNewsAsync(NewsDTO newsDTO, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            News news = await _repos.GetById(id);
            if (news == null) return BadRequest("These news doesn't exist");
            news.Title = newsDTO.Title;
            news.PublishDate = newsDTO.PublishDate.GetValueOrDefault();
            news.Description = newsDTO.Description;

            if (_repos.Update(news)) return Ok("/home/index");
            return BadRequest("Failed to update news");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteNewsAsync(int id)
        {
            News news = await _repos.GetById(id);
            if (news == null) return BadRequest("These news are already deleted");
            string fullPath = Path.GetFullPath("static" + news.ImagePath);
            System.IO.File.Delete(fullPath);
            if (await _repos.Delete(news)) return LocalRedirect("/home/index");
            return BadRequest("Failed to delete news");
        }
    }
}
