using System.Threading.Tasks;
using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.Remote.Interfaces
{
    public interface IDataSourceApi
    {
        /*
         * Game developer should be able to create a player. 
         */
        public UniTask<Player> CreatePlayer(Player dto);
        
        /*
         * Game developer should be able to fetch players info.
         */
        public UniTask<Player> GetPlayerInfo();
        
        /*
         * Game developer should be able to fetch player's NFTs.
         */
        public Task<NftCallResponse> GetPlayerNFTs(int page, int limit);

        /*
         * Developer should be able to fetch an specific NFT.
         */
        public UniTask<Nft> GetPlayerNFT(string id);

    }
}