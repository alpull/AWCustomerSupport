using System;
using AWCustomerSupport.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AWCustomerSupport {

    public class Program {

        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static void CreateDbIfNotExists(IHost host) {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                DbInitializer.Initialize(context);
            } catch (Exception e) {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, "Error creating the database.");
            }
        }

    }

}
