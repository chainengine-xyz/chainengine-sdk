using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class Token
    {
        [JsonProperty("sub")]
        public string Id;
        
        [JsonProperty("walletAddress")]
        public string WalletAddress;
        
        [JsonProperty("iat")]
        public string iat;
        
        [JsonProperty("exp")]
        public string exp;
    }
}