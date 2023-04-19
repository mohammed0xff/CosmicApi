using CosmicApi.Domain.Constants;

namespace CosmicApi.Configurations.Authentication.ApiKey
{
    public class InMemoryGetApiKeyQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;

        public InMemoryGetApiKeyQuery()
        {
            var existingApiKeys = new List<ApiKey>
        {
            new ApiKey(1, "CosmicStation", "31E197D4-2912-44E6-B210-4077C7A66738", new DateTime(2023, 01, 01),
                new List<string>
                {
                    Roles.Consumer,
                })
        };
            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
