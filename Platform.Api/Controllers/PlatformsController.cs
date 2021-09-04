using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platforms.Domain.Data;
using Platforms.Domain.DTOs;
using Platforms.Domain.Models;
using System.Collections.Generic;

namespace Platforms.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);

            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform([FromBody] PlatformCreateDto platform)
        {
            var platformToCreate = _mapper.Map<Platform>(platform);

            _repository.CreatePlatform(platformToCreate);

            var returnDto = _mapper.Map<PlatformReadDto>(platformToCreate);

            return CreatedAtAction(nameof(GetPlatformById), new { Id = returnDto.Id }, returnDto);
        }
    }
}
