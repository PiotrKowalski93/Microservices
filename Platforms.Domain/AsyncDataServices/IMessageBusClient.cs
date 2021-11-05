using Platforms.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforms.Domain.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platform);
    }
}
