using Newtonsoft.Json;

namespace ChainEngine.Model
{
    public class Player
    {
        [JsonProperty("walletAddress")] public string WalletAddress;

        [JsonProperty("gameId")]
        public string GameId;

        public string Token;
    }
}
