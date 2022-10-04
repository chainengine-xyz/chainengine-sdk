using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngine.Model
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
        public string OnChainId;

        [JsonProperty("chain")]
        public string Chain;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("supply")]
        public string Supply;

        [JsonProperty("holders")]
        public Hashtable Holders;

        [JsonProperty("metadata")]
        public NftMetadata Metadata;

        [JsonProperty("ownerPlayerId")]
        public string OwnerPlayerId;
    }
}
