using CosmicApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Api.Configurations
{
    public static class PersistanceConfig
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Infrastructure.IAssemblyMarker).Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
                });

                var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
                if (environment.IsDevelopment())
                    options.EnableDetailedErrors().EnableSensitiveDataLogging();
            });
            services.AddTransient<IContext, ApplicationDbContext>();

            return services;
        }
    }
}