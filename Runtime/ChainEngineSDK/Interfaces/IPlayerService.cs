using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.Interfaces
{
    public interface IPlayerService
    {
        /*
         * Game developer should be able to create a player. 
         */
        public UniTask<Player> CreateOrFetch(string walletAddress);

        /*
         * Game developer should be able to fetch player's NFTs.
         */
        public UniTask<NftCallResponse> GetNFTs(int page, int limit);

        /*
         * Developer should be able to fetch an specific NFT.
         */
        public UniTask<Nft> GetNFT(string chainId);
        
        public UniTask<string> GetNonce();
    } 
}