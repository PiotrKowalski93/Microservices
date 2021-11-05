using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            throw new NotImplementedException();
        }
    }

    enum EventType
    {
        Platform_Published,
        Undefined
    }
}
