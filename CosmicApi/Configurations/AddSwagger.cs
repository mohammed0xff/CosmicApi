using System;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;


namespace CosmicApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public interface IGuid { }
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
                            //Url = new Uri("")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            //Url = new Uri("")
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