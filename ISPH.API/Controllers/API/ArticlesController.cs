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
using Microsoft.EntityFrameworkCore;

namespace ISPH.API.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesRepository _repos;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public ArticlesController(IArticlesRepository repos, IWebHostEnvironment env, IMapper mapper)
        {
            _repos = repos;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<ArticleDTO>> GetAllArticles()
        {
            var art = await _repos.GetAll();
            return _mapper.Map<IList<ArticleDTO>>(art);
        }

        [HttpGet("id={id}")]
        public async Task<Article> GetArticleById(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
       // [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> AddArticle([FromForm] ArticleDTO art)
        {
            if(!ModelState.IsValid) return BadRequest("Fill all fields");
            string path = "/images/" + art.File.FileName;
            Article article = new Article()
            {
                Title = art.Title,
                PublishDate = art.PublishDate,
                Description = art.Description,
                ImagePath = path
            };
            if (await _repos.HasEntity(article)) return BadRequest("This article already exists");
            using (var stream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                await art.File.CopyToAsync(stream);
            }
            if (await _repos.Create(article)) return LocalRedirect("/home/index");

            return BadRequest("Failed to add article");
        }


        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateArticle(ArticleDTO art, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            Article article = await _repos.GetById(id);
            if (article == null) return BadRequest("This article doesn't exist");

             article.Title = art.Title;
             article.PublishDate = art.PublishDate;
             article.Description = art.Description;
            if (_repos.Update(article)) return LocalRedirect("/home/index");

            return BadRequest("Failed to update article");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            Article article = await _repos.GetById(id);
            if (article == null) return BadRequest("This article is already deleted");
            string fullPath = Path.GetFullPath("static" + article.ImagePath);
            System.IO.File.Delete(fullPath);
            if (await _repos.Delete(article)) return LocalRedirect("/home/index");
            return BadRequest("Failed to delete article");
        }
    }
}
