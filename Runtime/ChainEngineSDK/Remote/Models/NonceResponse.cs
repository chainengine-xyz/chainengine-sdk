using Newtonsoft.Json;

namespace ChainEngineSDK.Remote.Models
{
    public class NonceResponse
    {
        [JsonProperty("nonce")]
        public string Nonce;
    }
}