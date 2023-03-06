namespace CosmicApi.Configurations
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            
            return services;
        }

    }
}
