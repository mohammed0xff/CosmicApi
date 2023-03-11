using FluentValidation;

namespace CosmicApi.Configurations
{
    public static class ValidationConfig
    {
        public static void AddValidation(this IMvcBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<CosmicApi.Application.IAssemblyMarker>();
        }
    }
}
