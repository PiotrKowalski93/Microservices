using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platforms.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Platforms.Domain.Data
{
    public static class MockDb
    {
        public static void SetUpDb(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }

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
