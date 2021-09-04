using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platforms.Domain.Models;
using System;
using System.Linq;

namespace Platforms.Domain.Data
{
    public static class MockDb
    {
        public static void SetUpDb(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("----- Seeding data -----");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Docker", Publisher = "CNCF", Cost = "5$" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Db is filled with data");
            }
        }
    }
}
