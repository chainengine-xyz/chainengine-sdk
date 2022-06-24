using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineApi.Model
{
    [Preserve]
    public class Nft
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
        public Dictionary<string, object> Metadata;
        
        [JsonProperty("ownerPlayerId")]
        public string OwnerPlayerId;
        
        public object GetMetadataValue(string field)
        {
            return Metadata.GetValueOrDefault(field);
        }
    }
}