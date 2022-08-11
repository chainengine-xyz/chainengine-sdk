using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngine.Model
{
    [Preserve]
    public class NftMetadata
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("image")]
        public string Image;

        [JsonProperty("attributes")]
        public Dictionary<string, object> Attributes;

        [JsonProperty("URI")]
        public string Uri;

        public object GetAttributeValue(string field)
        {
            return Attributes.TryGetValue(field, out var value) ? value : null;
        }
    }
}
