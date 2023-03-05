﻿using CosmicApi.Infrastructure.Common;
using CosmicApi.Infrastructure.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CosmicApi.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurationSection = configuration.GetSection("TokenConfiguration");
            
            // configure token values
            services.Configure<TokenConfiguration>(
                tokenConfigurationSection
            );

            var tokenConfig = tokenConfigurationSection.Get<TokenConfiguration>();
            var key = Encoding.UTF8.GetBytes(tokenConfig!.Secret);
            
            // add TokenValidationParameters to service collection
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = tokenConfig.Issuer,
                ValidAudience = tokenConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // add tokenValidationParams as a singleton
            services.AddSingleton(tokenValidationParams);
            
            // add ITokenService to service collection
            services.AddTransient<ITokenService, JwtService>();

            // configure jwt authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParams;
                });

            return services;
        }

    }
}
