using AutoMapper;
using Commands.Domain.Data;
using Commands.Domain.DTOs;
using Commands.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace Commands.Api.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = IndentifyEvent(message);
            
            switch (eventType)
            {
                case EventType.Platform_Published:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType IndentifyEvent(string message)
        {
            Console.WriteLine("IndentifyEvent: ");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("Platform_Published event Detected");
                    return EventType.Platform_Published;
                default:
                    Console.WriteLine("Cannot identify event");
                    return EventType.Undefined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDto);

                    if(!repo.IsPlatformExists(platform.Id))
                    {

                    }
                    else
                    {
                        Console.WriteLine($"Platform with Id: {platform.Id} already exist");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cant save published platform to DB. {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        Platform_Published,
        Undefined
    }
}
