using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("{controller}/")]
    [Authorize]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementsRepository _repos;
        private readonly IPositionsRepository _positionRepos;
        private readonly IMapper _mapper;
        public AdvertisementsController(IAdvertisementsRepository advRepos, IPositionsRepository positionRepos, IMapper mapper)
        {
            _repos = advRepos;
            _positionRepos = positionRepos;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<AdvertisementDTO>> GetAllAdvertisements()
        {
            var ads = await _repos.GetAll();
            return _mapper.Map<IList<AdvertisementDTO>>(ads);
        }

        [HttpGet("pos={id}")]
        [AllowAnonymous]
        public async Task<IList<AdvertisementDTO>> GetAdvertisementsForPosition(int id)
        {
            var ads = await _repos.GetAdvertisementsForPosition(id);
            return _mapper.Map<IList<AdvertisementDTO>>(ads);
        }
        [HttpGet("emp={id}")]
        [AllowAnonymous]
        public async Task<IList<AdvertisementDTO>> GetAdvertisementsByEmployer(int id)
        {
            var ads = await _repos.GetAdvertisementsForEmployer(id);
            return _mapper.Map<IList<AdvertisementDTO>>(ads);
        }
        [HttpGet("com={id}")]
        [AllowAnonymous]
        public async Task<IList<AdvertisementDTO>> GetAllAdvertisementsForCompany(int id)
        {
            var ads = await _repos.GetAdvertisementsForCompany(id);
            return _mapper.Map<IList<AdvertisementDTO>>(ads);
        }
        [HttpGet("search={name}")]
        [AllowAnonymous]
        public async Task<IList<AdvertisementDTO>> GetAdvertisementsForSearchValue(string name)
        {
            name = name.ToLower();
            name = char.ToUpper(name[0]) + name.Substring(1);
            var ads = await _repos.GetAll();
            var filtered = ads.Where(ad => ad.PositionName.Contains(name) || ad.Employer.CompanyName.Contains(name));
            return _mapper.Map<IList<AdvertisementDTO>>(filtered);
        }


        [HttpPost("emp={id}/add")]
        public async Task<IActionResult> AddAdvertisement(AdvertisementDTO adv, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position pos = await _positionRepos.GetPositionByName(adv.PositionName);
            if (pos == null) return BadRequest("Such position is not in database");
            var advertisement = new Advertisement()
            {
                Title = adv.Title,
                Salary = adv.Salary.GetValueOrDefault(),
                Description = adv.Description,
                PositionName = adv.PositionName,
                PositionId = pos.PositionId,
                EmployerId = id
            };
            if (await _repos.HasEntity(advertisement)) return BadRequest("Ads with this title already exists");
            if (await _repos.Create(advertisement)) return Ok("Added new ads");
            return BadRequest("Failed to add ads");
        }


        [HttpGet("id={id}")]
        [AllowAnonymous]
        public async Task<AdvertisementDTO> GetAdvertisementById(int id)
        {
            var ad = await _repos.GetById(id);
            return _mapper.Map<AdvertisementDTO>(ad);
        }
        [HttpPut("id={id}/update")]
        public async Task<IActionResult> UpdateAdvertisement(AdvertisementDTO advertisement, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            Advertisement ad = await _repos.GetById(id);
            if(ad != null)
            {
                ad.Title = advertisement.Title;
                ad.Description = advertisement.Description;
                ad.Salary = advertisement.Salary.GetValueOrDefault();
                ad.PositionName = advertisement.PositionName;
                if (await _repos.Update(ad)) return Ok("Updated ads");
            }
            return BadRequest("This ads is not in database");
        }

        [HttpDelete("id={id}/delete")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            Advertisement ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads already deleted");
            if (await _repos.Delete(ad)) return Ok("Deleted ads");
            return BadRequest("Failed to delete ads");
        }
    }
}
