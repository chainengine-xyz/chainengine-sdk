using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.Model
{
    [Preserve]
    public class Nft
    {
        [JsonProperty("id")]
        public string Id;
        
        [JsonProperty("accountId")]
        public string AccountTd;
        
        [JsonProperty("gameId")]
        public string GameId;
        
        [JsonProperty("onChainId")]
        public string ChainId;
        
        [JsonProperty("chain")]
        public string Chain;
        
        [JsonProperty("status")]
        public string Status;
        
        [JsonProperty("metadata")]
        public NftMetadata Metadata;
        
        [JsonProperty("ownerPlayerId")]
        public string OwnerPlayerId;
    }
}