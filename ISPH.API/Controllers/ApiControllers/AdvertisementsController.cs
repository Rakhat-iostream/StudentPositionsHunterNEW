using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using ISPH.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("{controller}/")]
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
        public async Task<IList<AdvertisementDto>> GetAdvertisementsPerPage(int page)
        {
            IList<Advertisement> ads;
            if (page == 0) ads = await _repos.GetAll();
            else ads = await _repos.GetAdvertisementsPerPage(page);
            return _mapper.Map<IList<AdvertisementDto>>(ads);
        }

        [HttpGet("count")]
        public async Task<int> GetAdvertisementsCount()
        {
            return await _repos.GetAdvertisementsCount();
        }

        [HttpGet("amount={amount}")]
        public async Task<IList<AdvertisementDto>> GetAdvertisementsAmount(int amount)
        {
            var ads = await _repos.GetAdvertisementsAmount(amount);
            return _mapper.Map<IList<AdvertisementDto>>(ads);
        }

        [HttpGet("pos={id}")]
        public async Task<IList<AdvertisementDto>> GetAdvertisementsForPosition(int id)
        {
            var ads = await _repos.GetAdvertisementsByEntityId(id, EntityType.Position);
            return _mapper.Map<IList<AdvertisementDto>>(ads);
        }
        [HttpGet("emp={id}")]
        public async Task<IList<AdvertisementDto>> GetAdvertisementsByEmployer(int id)
        {
            var ads = await _repos.GetAdvertisementsByEntityId(id, EntityType.Employer);
            return _mapper.Map<IList<AdvertisementDto>>(ads);
        }
        [HttpGet("com={id}")]
        public async Task<IList<AdvertisementDto>> GetAllAdvertisementsForCompany(int id)
        {
            var ads = await _repos.GetAdvertisementsByEntityId(id, EntityType.Company);
            return _mapper.Map<IList<AdvertisementDto>>(ads);
        }
        [HttpGet("search={value}")]
        public async Task<IList<AdvertisementDto>> GetAdvertisementsForSearchValue(string value)
        {
            value = value.ToLower();
            value = char.ToUpper(value[0]) + value.Substring(1);
            var filtered = await _repos.GetFilteredAdvertisements(value);
            return _mapper.Map<IList<AdvertisementDto>>(filtered);
        }
        [HttpPost("filter")]
        public async Task<IList<AdvertisementDto>> GetFilteredAdvertisements(FilteredAdvertisementDto dto)
        {
            var ads = await _repos.GetFilteredAdvertisements(dto);
            return _mapper.Map<IList<AdvertisementDto>>(ads);
        }



        [HttpPost("emp={id}/add")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> AddAdvertisement(AdvertisementDto adv, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position pos = await _positionRepos.GetPositionByName(adv.PositionName);
            if (pos == null) return BadRequest("Such position is not in database");
            var advertisement = new Advertisement()
            {
                Title = adv.Title,
                Salary = adv.Salary.Value,
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
        public async Task<AdvertisementDto> GetAdvertisementById(int id)
        {
            var ad = await _repos.GetById(id);
            return _mapper.Map<AdvertisementDto>(ad);
        }
        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> UpdateAdvertisement(AdvertisementDto advertisement, int id)
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
        [Authorize(Roles = RoleType.Employer)]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            Advertisement ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads already deleted");
            if (await _repos.Delete(ad)) return Ok("Deleted ads");
            return BadRequest("Failed to delete ads");
        }
    }
}
