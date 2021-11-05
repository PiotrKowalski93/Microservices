using System.Collections.Generic;
using Commands.Domain.Models;

namespace Commands.Domain.Data
{
    public interface ICommandRepo
    {
        // Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool IsPlatformExists(int platformId);

        // Commands
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}
