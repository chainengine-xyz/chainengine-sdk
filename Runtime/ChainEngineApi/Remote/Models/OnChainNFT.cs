using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.ChainEngineApi.Remote.Models
{
    [Preserve]
    public class OnChainNFT
    {
        [JsonProperty("token_address")]
        public string TokenAddress;
        
        [JsonProperty("token_id")]
        public string TokenId;
        
    }
}