using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.ChainEngineApi.Remote.Models
{
    [Preserve]
    public class OnChainNFTCallResponse
    {
        [JsonProperty("items")]
        public List<OnChainNFTListResponse> Items;
    }
}