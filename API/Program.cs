﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var ctx = scope.ServiceProvider.GetRequiredService<DataContext>(); 
                    ctx.Database.Migrate();
                    Seed.SeedData(ctx);
                }
                catch (Exception ex)
                {
                    var log = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    log.LogError(ex, "Failed to update migration.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
