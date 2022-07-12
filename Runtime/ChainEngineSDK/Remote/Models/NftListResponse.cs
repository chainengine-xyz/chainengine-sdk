using System.Collections.Generic;
using ChainEngineSDK.Model;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineSDK.Remote.Models
{
    [Preserve]
    public class ChainNftListResponse
    {
        [JsonProperty("nfts")]
        public List<Nft> Nfts;
    }
}