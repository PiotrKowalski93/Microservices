using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platforms.Api.Http;
using Platforms.Domain.Data;
using Platforms.Domain.DTOs;
using Platforms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platforms.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandDataClient _commandDataClient;
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _commandDataClient = commandDataClient;
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

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatformWithCommand([FromBody] PlatformCreateDto platform)
        {
            var platformToCreate = _mapper.Map<Platform>(platform);

            _repository.CreatePlatform(platformToCreate);

            var returnDto = _mapper.Map<PlatformReadDto>(platformToCreate);

            try
            {
                await _commandDataClient.SendPlatformToCommand(returnDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return CreatedAtAction(nameof(GetPlatformById), new { Id = returnDto.Id }, returnDto);
        }
    }
}
