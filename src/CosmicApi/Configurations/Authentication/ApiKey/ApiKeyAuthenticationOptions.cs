using Microsoft.AspNetCore.Authentication;

namespace CosmicApi.Configurations.Authentication.ApiKey
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "ApiKey";
        public const string ApiKeyHeaderName = "X-Api-Key";
        public string ApiKey { get; set; }
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
