using Microsoft.EntityFrameworkCore;
using Platforms.Domain.Models;

namespace Platforms.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
