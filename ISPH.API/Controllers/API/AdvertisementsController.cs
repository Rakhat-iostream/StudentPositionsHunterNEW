using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace ISPH.API.Controllers
{
    [Route("{controller}/")]
    [Authorize]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementsRepository _repos;
        private readonly IPositionsRepository _positionRepos;
        public AdvertisementsController(IAdvertisementsRepository advRepos, IPositionsRepository positionRepos)
        {
            _repos = advRepos;
            _positionRepos = positionRepos;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAllAdvertisements()
        {
            return await _repos.GetAll();
        }

        [HttpGet("pos={id}")]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAdvertisementsForPosition(int id)
        {
            return await _repos.GetAdvertisementsForPosition(id);
        }
        [HttpGet("emp={id}")]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAdvertisementsByEmployer(int id)
        {
            return await _repos.GetAdvertisementsForEmployer(id);
        }
        [HttpGet("com={id}")]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAllAdvertisementsForCompany(int id)
        {
            return await _repos.GetAdvertisementsForCompany(id);
        }
        [HttpGet("search={name}")]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAdvertisementsForSearchValue(string name)
        {
            var ads = await _repos.GetAll();
            var adsForPos = ads.Where(ad => ad.PositionName.Contains(name));
            var adsForCom = ads.Where(ad => ad.Employer.CompanyName.Contains(name));
            return adsForPos.Union(adsForCom).ToList();
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
        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            return await _repos.GetById(id);
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
                if (_repos.Update(ad)) return Ok("Updated ads");
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
