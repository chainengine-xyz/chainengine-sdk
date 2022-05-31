namespace ChainEngineSDK.ChainEngineApi.Model
{
    public class OnChainNFTMetadata
    {
        public string Name;
        public string Description;
        public string Image;
    }
    
    public class OnChainNFT
    {
        public string TokenAddress;
        public string TokenID;
        public string Amount;
        
        public OnChainNFTMetadata Metadata;
    }
}