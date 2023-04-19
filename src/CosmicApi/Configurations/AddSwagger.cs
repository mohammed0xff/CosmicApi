using System;
using System.Reflection;
using CosmicApi.Configurations.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;


namespace CosmicApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Cosmic.Api",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "mohammed0xff",
                            Url = new Uri("https://github.com/mohammed0xff")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            Url = new Uri("https://github.com/mohammed0xff/CosmicApi/blob/master/LICENSE.txt")
                        }
                    });

                options.DescribeAllParametersInCamelCase();
                options.OrderActionsBy(x => x.RelativePath);

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                options.CustomSchemaIds(type => type.ToString());

                // add JWT bearer authorization 
                // https://stackoverflow.com/a/58667736

                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme JwtScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                OpenApiSecurityScheme apiKeyScheme = new()
                {
                    Name = "X-Api-Key",
                    Scheme = ApiKeyAuthenticationOptions.DefaultScheme,
                    Description = "Api key needed to access the endpoints. X-Api-Key: My_API_Key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Reference = new()
                    {
                        Id = "X-Api-Key",
                        Type = ReferenceType.SecurityScheme,
                    }
                };
                options.AddSecurityDefinition(ApiKeyAuthenticationOptions.ApiKeyHeaderName, apiKeyScheme);


                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {JwtScheme, new string[] { }}, {apiKeyScheme, new string[] { }},
                };
                options.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.DisplayRequestDuration();
                    options.DocExpansion(DocExpansion.List);
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = "swagger";
                });

            return app;
        }
    }
}
