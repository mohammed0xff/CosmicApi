using CosmicApi.Application.Common;
using CosmicApi.Application.Common.Behaviors;
using CosmicApi.Application.Common.Handlers;
using MediatR;

namespace CosmicApi.Configurations
{
    public static class MediatRConfig
    {
        public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CosmicApi.Application.IAssemblyMarker).Assembly);
            services.AddScoped<INotificationHandler<ValidationError>, ValidationErrorHandler>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            return services;
        }
    }
}
