using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CosmicApi.Application.Common.Behaviors;
using CosmicApi.Application.Common.Handlers;
using CosmicApi.Application.Common;

namespace CosmicApi.Configurations
{
    public static class MediatRConfig
    {
        public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CosmicApi.Application.IAssemblyMarker).Assembly);

            return services;
        }
    }
}
