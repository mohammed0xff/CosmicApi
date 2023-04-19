using CosmicApi.Application.Common.Session;
using CosmicApi.Configurations.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ISession = CosmicApi.Application.Common.Session.ISession;

namespace CosmicApi.Configurations
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISession, Session>();
            services.AddAuthorization(options =>
            {
                var onlyJwtBearerSchemePolicyBuilder = 
                    new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                options.AddPolicy("OnlyJwtBearerScheme", onlyJwtBearerSchemePolicyBuilder
                    .RequireAuthenticatedUser()
                    .Build());

                var onlyApiKeySchemePolicyBuilder = 
                    new AuthorizationPolicyBuilder(ApiKeyAuthenticationOptions.DefaultScheme);
                options.AddPolicy("OnlyApiKeyScheme", onlyApiKeySchemePolicyBuilder
                    .RequireAuthenticatedUser()
                    .Build());
            });

            return services;
        }

    }
}
