using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class SignatureResponse
    {
        [JsonProperty("token")] [CanBeNull] public string Token;
        
        [JsonProperty("error")] [CanBeNull] public string Error;
    }
}