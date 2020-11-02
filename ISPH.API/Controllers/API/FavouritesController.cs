using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers.API
{
    [Route("/users/students/id={id}/[controller]/")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesRepository _repos;
        public FavouritesController(IFavouritesRepository repos)
        {
            _repos = repos;
        }


        [HttpGet]
        public async Task<IList<Advertisement>> GetFavourites(int id)
        {
            var favs = await _repos.GetFavourites(id);
            return favs.Select(fav => fav.Advertisement).ToList();
        }


        [HttpGet("ad={adId}")]
        public async Task<FavouriteAdvertisement> GetFavourite(int id, int adId)
        {
            return await _repos.GetById(id, adId);
        }

        [HttpPost("ad={adId}/add")]
        public async Task<IActionResult> AddToFavourites(int id, int adId)
        {
            var fav = new FavouriteAdvertisement() { AdvertisementId = adId, StudentId = id };
            if (await _repos.AddToFavourites(fav)) return LocalRedirect("/home/advertisements/id=" + adId);
            return BadRequest("Failed to add to favourites");
        }

        [HttpPost("ad={adId}/delete")]
        public async Task<IActionResult> DeleteToFavourites(int id, int adId)
        {
            var fav = await _repos.GetById(id, adId);
            if (await _repos.DeleteFromFavourites(fav)) return LocalRedirect("/home/advertisements/id=" + adId);
            return BadRequest("Failed to add to favourites");
        }
    }
}
