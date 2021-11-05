using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platforms.Api.Http;
using Platforms.Domain.AsyncDataServices;
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
        private readonly IMessageBusClient _messageBusClient;
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repository, 
            IMapper mapper, 
            ICommandDataClient commandDataClient, 
            IMessageBusClient messageBusClient)
        {
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
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
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platform)
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
                Console.WriteLine($"Could not send sync request to commandsService: {ex.Message}");
            }

            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(returnDto);
                platformPublishedDto.Event = "Platform_Published";

                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not send sync request to commandsService: {ex.Message}");
            }

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
