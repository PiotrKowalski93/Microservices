using AutoMapper;
using Commands.Domain.DTOs;
using Commands.Domain.Models;

namespace Commands.Api.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Platform, PlatformReadDto>();
        }
    }
}
