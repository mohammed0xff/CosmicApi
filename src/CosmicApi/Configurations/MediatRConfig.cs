using MediatR;

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
