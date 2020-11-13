using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers.API
{
    [Route("/users/students/id={studentId}/[controller]/")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesRepository _repos;
        private readonly IMapper _mapper;
        public FavouritesController(IFavouritesRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IList<AdvertisementDTO>> GetFavourites(int studentId)
        {
            var favs = await _repos.GetFavourites(studentId);
            return _mapper.Map<IList<AdvertisementDTO>>(favs.Select(fav => fav.Advertisement));
        }


        [HttpGet("ad={adId}")]
        public async Task<FavouriteAdvertisement> GetFavourite(int studentId, int adId)
        {
            return await _repos.GetById(studentId, adId);
        }

        [HttpPost("ad={adId}/add")]
        [Authorize(Roles = RoleType.Student)]
        public async Task<IActionResult> AddToFavourites(int studentId, int adId)
        {
            var fav = new FavouriteAdvertisement() { AdvertisementId = adId, StudentId = studentId };
            if (await _repos.AddToFavourites(fav)) return LocalRedirect("/home/advertisements/id=" + adId);
            return BadRequest("Failed to add to favourites");
        }

        [HttpPost("ad={adId}/delete")]
        [Authorize(Roles = RoleType.Student)]
        public async Task<IActionResult> DeleteFromFavourites(int studentId, int adId)
        {
            var fav = await _repos.GetById(studentId, adId);
            if (await _repos.DeleteFromFavourites(fav)) return LocalRedirect("/home/advertisements/id=" + adId);
            return BadRequest("Failed to add to favourites");
        }
    }
}
