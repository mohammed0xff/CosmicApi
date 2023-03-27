using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace CosmicApi.Domain.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GalaxyType
    {
        other,
        elliptical,
        spiral,
        irregular
    }
}
