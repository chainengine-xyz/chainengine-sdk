using Newtonsoft.Json;

namespace ChainEngineSDK.Model
{
    public class Player
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress;
        
        [JsonProperty("gameId")]
        public string GameId;
        
        [JsonProperty("apiKey")]
        public string APIKey;
    }
}