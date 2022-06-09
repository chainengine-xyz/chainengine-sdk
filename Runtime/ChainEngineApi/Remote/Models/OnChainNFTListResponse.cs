using System.Collections.Generic;
using ChainEngineSDK.ChainEngineApi.Model;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.ChainEngineApi.Remote.Models
{
    [Preserve]
    public class OnChainNFTListResponse
    {
        [JsonProperty("nfts")]
        public List<OnChainNFT> nfts;
    }
}