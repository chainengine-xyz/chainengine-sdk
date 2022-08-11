using ChainEngine.Model;
using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class NewPlayerResponse
    {
            [JsonProperty("player")]
            public Player Player;
    }
}
