using Newtonsoft.Json;

namespace ChainEngine.Model
{
    public class Player
    {
        [JsonProperty("id")]
        public string Id;
        
        [JsonProperty("walletAddress")]
        public string WalletAddress;

        public string Token;
    }
}
