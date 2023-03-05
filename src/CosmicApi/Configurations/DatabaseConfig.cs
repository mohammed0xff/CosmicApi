using CosmicApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Configurations
{
    public static class DatabaseConfigurationExtension
    {

        public static async Task InitDatabase(this IApplicationBuilder app)
        {
            await using var serviceScope = app.ApplicationServices.CreateAsyncScope();
            await using var context = serviceScope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            // Ensure db created (commented : cause MigrateAsync() throws error if db was already created).
            // await context.Database.EnsureCreatedAsync();

            // if there are no applied migrations .. apply them all
            if (!(await context.Database.GetAppliedMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }
            
            // Ensure data seed 
            await context.EnsureSeedDataAsync();

            // logging that later
            var connectionString = context.Database.GetConnectionString();
        }
    }
}
