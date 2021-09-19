using Microsoft.Extensions.Configuration;
using Platforms.Domain.DTOs;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Platforms.Api.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                    JsonSerializer.Serialize(platform),
                    Encoding.UTF8,
                    "application/json"
                );

            var response = await _httpClient.PostAsync($"{_config["CommandsApiEndpoint"]}/api/com/platforms/test", httpContent);
        }
    }
}
