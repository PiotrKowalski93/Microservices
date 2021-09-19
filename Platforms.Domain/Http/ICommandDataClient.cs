using Platforms.Domain.DTOs;
using System.Threading.Tasks;

namespace Platforms.Api.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platform);
    }
}
