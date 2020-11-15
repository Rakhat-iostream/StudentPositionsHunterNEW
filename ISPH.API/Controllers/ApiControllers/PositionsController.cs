using System.Collections.Generic;
using System.Threading.Tasks;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ISPH.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("[controller]/")]
    [Authorize(Roles = RoleType.Admin)]
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
        [AllowAnonymous]
        public async Task<IList<PositionDto>> GetAllPositionsAsync()
        {
            var positions = await _repos.GetAll();
            return _mapper.Map<IList<PositionDto>>(positions);
        }

        [HttpGet("id={id}")]
        [AllowAnonymous]
        public async Task<PositionDto> GetPositionByIdAsync(int id)
        {
            var pos = await _repos.GetById(id);
            return _mapper.Map<PositionDto>(pos);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPositionAsync(PositionDto pos)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position position = new Position()
            {
                Name = pos.Name
            };
            if (await _repos.HasEntity(position)) return BadRequest("Position is already in database");
            if (await _repos.Create(position)) return Ok("Added new position");
            return BadRequest("Failed to add position");
        }

        [HttpPut("id={id}/update")]
        public async Task<IActionResult> UpdatePositionAsync(PositionDto pos, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position position = await _repos.GetById(id);
            if (!(await _repos.HasEntity(position))) return BadRequest("Position doesn't exist");
            position.Name = pos.Name;
            if (await _repos.Update(position)) return Ok("Updated Position");
            return BadRequest("Failed to add Position");
        }

        [HttpDelete("id={id}/delete")]
        public async Task<IActionResult> DeletePositionAsync(int id)
        {
            Position position = await _repos.GetById(id);
            if (position == null) return BadRequest("This position is already deleted");
            if (await _repos.Delete(position)) return Ok("Deleted position");
            return BadRequest("Failed to delete position");
        }
    }
}
