using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.ChainEngineApi.Remote.Models
{
    [Preserve]
    public class OffChainNFT
    {
        [JsonProperty("id")]
        public string Id;
        
        [JsonProperty("onChainId")]
        public string OnChainId;
        
        [JsonProperty("chain")]
        public string Chain;
        
        [JsonProperty("status")]
        public string Status;
        
        [JsonProperty("metadata")]
        public object Metadata;
        
        [JsonProperty("ownerPlayerId")]
        public string OwnerPlayerId;
        
    }
}