using CosmicApi.Application.Common.Session;
using ISession = CosmicApi.Application.Common.Session.ISession;

namespace CosmicApi.Configurations
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISession, Session>();
            services.AddAuthorization();
            
            return services;
        }

    }
}
