using ChainEngine.Model;
using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class NewPlayerRequest
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress;

        [JsonProperty("gameId")]
        public string GameId;
    }
    
    public class NewPlayerResponse
    {
            [JsonProperty("player")]
            public Player Player;
    }
}
