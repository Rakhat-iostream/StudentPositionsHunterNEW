using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISPH.API.Controllers.API
{
    [Route("[controller]/")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsRepository _repos;
        private readonly IMapper _mapper;
        public PositionsController(IPositionsRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<PositionDTO>> GetAllPositionsAsync()
        {
            var pos = await _repos.GetAll();
            return _mapper.Map<IList<PositionDTO>>(pos);
        }

        [HttpGet("id={id}")]
        public async Task<Position> GetPositionByIdAsync(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPositionAsync(PositionDTO pos)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position position = new Position()
            {
                Name = pos.Name,
                Amount = 1,
            };
            if (await _repos.HasEntity(position)) return BadRequest("Position is already in database");
            if (await _repos.Create(position)) return Ok("Added new position");
            return BadRequest("Failed to add position");
        }

        [HttpPut("id={id}/update")]
        public async Task<IActionResult> UpdatePositionAsync(PositionDTO pos, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position position = await _repos.GetById(id);
            if (!(await _repos.HasEntity(position))) return BadRequest("Position doesn't exist");
            position.Name = pos.Name;
            if (_repos.Update(position)) return Ok("Updated Position");
            return BadRequest("Failed to add Position");
        }

        [HttpDelete("id={id}/delete")]
        public async Task<IActionResult> DeletePositionAsync(int id)
        {
            Position position = await GetPositionByIdAsync(id);
            if (position == null) return BadRequest("This position is already deleted");
            if (await _repos.Delete(position)) return Ok("Deleted position");
            return BadRequest("Failed to delete position");
        }
    }
}
