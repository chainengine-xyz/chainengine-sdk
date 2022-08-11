using System.Collections.Generic;
using ChainEngine.Model;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngine.Remote.Models
{
    [Preserve]
    public class ChainNftListResponse
    {
        [JsonProperty("nfts")]
        public List<Nft> Nfts;
    }
}
