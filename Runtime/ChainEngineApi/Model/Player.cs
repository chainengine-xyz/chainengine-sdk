using Newtonsoft.Json;

namespace ChainEngineSDK.ChainEngineApi.Model
{
    public class Player
    {
        private string _id;
        
        [JsonProperty("walletAddress")]
        public string WalletAddress;
        
        [JsonProperty("accountId")]
        public string AccountId;
        
        [JsonProperty("gameId")]
        public string GameId;
        
        [JsonProperty("apiKey")]
        public string apiKey;

        public string GetId()
        {
            return _id;
        }
    }
}