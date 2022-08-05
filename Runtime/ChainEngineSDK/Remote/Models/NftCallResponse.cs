using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.Remote.Models
{
    [Preserve]
    public class NftCallResponse
    {

        [JsonProperty("total")]
        public int Total;
        
        [JsonProperty("offset")]
        public int Offset;
        
        [JsonProperty("page")]
        public int Page;
        
        [JsonProperty("items")]
        public List<ChainNftListResponse> Items;
    }
}