using AutoMapper;
using Commands.Domain.Data;
using Commands.Domain.DTOs;
using Commands.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Commands.Api.Controllers
{
    [ApiController]
    [Route("api/com/platforms/{platformId}/[controller]/[action]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> GetCommandsForPlatform({platformId})");

            if(!_commandRepo.IsPlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _commandRepo.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandsForPlatform")]
        public ActionResult<CommandReadDto> GetCommandsForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> GetCommandsForPlatform({platformId}, {commandId})");

            if (!_commandRepo.IsPlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _commandRepo.GetCommand(platformId, commandId);

            if(commands == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(commands));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> CreateCommandForPlatform({platformId}, {commandDto.CommandLine})");

            if (!_commandRepo.IsPlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _commandRepo.CreateCommand(platformId, command);

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandsForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }
}
