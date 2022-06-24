using System.Collections.Generic;
using ChainEngineApi.Model;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngineApi.Remote.Models
{
    [Preserve]
    public class ChainNftListResponse
    {
        [JsonProperty("nfts")]
        public List<Nft> Nfts;
    }
}