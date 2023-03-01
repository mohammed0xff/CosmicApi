using CosmicApi.Infrastructure.Services.TokenService;

namespace CosmicApi.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, JwtService>();
            
            return services;
        }
    }
}
