using CosmicApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Configurations
{
    public static class DatabaseConfigurationExtension
    {
        public static async Task InitDatabase(this IApplicationBuilder app, ILogger logger)
        {
            await using var serviceScope = app.ApplicationServices.CreateAsyncScope();
            await using var context = serviceScope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
            
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Apply migrations if non was applyied.
            var migrations = await context.Database.GetAppliedMigrationsAsync();
            if (migrations.Count() == 0)
            {
                logger.LogInformation("======= Applying Migrations ===== ");
                await context.Database.MigrateAsync();
            }

            // Ensure data seed 
            await context.EnsureSeedDataAsync(logger);
            
            logger.LogInformation(
                $"DB connection string :{context.Database.GetConnectionString()}"
                );
        }
    }
}
