using Commands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commands.Domain.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(int platformId, Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;

            _context.Commands.Add(command);

            if(!SaveChanges())
            {
                throw new NotSupportedException($"Save changes failed at CreateCommand({platformId}, {command.CommandLine})");
            }
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);

            if (!SaveChanges())
            {
                throw new NotSupportedException($"Save changes failed at CreatePlatform({platform.Name})");
            }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).SingleOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId);
        }

        public bool IsExternalPlatformExists(int externalId)
        {
            return _context.Platforms.Any(p => p.ExternalId == externalId);
        }

        public bool IsPlatformExists(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        private bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
