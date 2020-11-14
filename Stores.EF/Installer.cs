using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DevKnack.Stores.EF
{
    public static class StoresInstaller
    {
        public static async Task MigrateAsync(IServiceScopeFactory scopeFactory, bool dropDatabase)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("StoresInstaller");

                using (var context = provider.GetService<StoresDbContext>())
                {
                    if (dropDatabase)
                    {
                        logger.LogInformation("Drop database ...");
                        await context.Database.EnsureDeletedAsync();
                    }
                    logger.LogInformation("Create database");
                    await context.Database.MigrateAsync();
                }
            }
        }
    }
}