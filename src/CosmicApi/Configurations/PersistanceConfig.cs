using CosmicApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Api.Configurations;

public static class PersistanceConfig
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddTransient<IContext, ApplicationDbContext>();

        return services;
    }
}