using ChainEngineSDK.Model;
using Newtonsoft.Json;

namespace ChainEngineSDK.Remote.Models
{
    public class NewPlayerResponse
    {
            [JsonProperty("player")]
            public Player Player;
    }
}