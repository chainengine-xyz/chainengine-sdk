using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class NonceRequest
    {
        [JsonProperty("gameId")]
        public string GameId;
    }

    public class NonceResponse
    {
        [JsonProperty("nonce")]
        public string Nonce;
    }
}
