using Newtonsoft.Json;

namespace ChainEngine.Remote.Models
{
    public class SignedTransferRequest
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress;
        
        [JsonProperty("nftId")]
        public string NftId;
        
        [JsonProperty("amount")]
        public int Amount;
    }
    
    public class SignedTransferResponse
    {
        [JsonProperty("id")]
        public string Id;
    }
}