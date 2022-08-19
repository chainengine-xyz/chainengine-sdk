using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class AuthSocket
    {
        [JsonProperty("error")] [CanBeNull] public string Error;

        [JsonProperty("token")] [CanBeNull] public string Token;
    }
}
