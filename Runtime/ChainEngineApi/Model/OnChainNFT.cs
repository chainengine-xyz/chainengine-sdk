namespace ChainEngineSDK.ChainEngineApi.Model
{
    public class OnChainNFTMetadata
    {
        public string name;
        public string description;
        public string image;
    }
    
    public class OnChainNFT
    {
        public string token_address;
        public string token_id;
        public string amount;
        
        public OnChainNFTMetadata metadata;
    }
}