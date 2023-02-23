using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;


namespace CosmicApi.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Cosmic.Api",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "",
                            Url = new Uri("")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            Url = new Uri("")
                        }
                    });
                c.DescribeAllParametersInCamelCase();
                c.OrderActionsBy(x => x.RelativePath);

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                    c.DocExpansion(DocExpansion.List);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    c.RoutePrefix = "api-docs";
                });

            return app;
        }
    }
}