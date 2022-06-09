using System.Collections;
using System.Collections.Generic;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using Cysharp.Threading.Tasks;
using Nethereum.RPC.Eth;
using OnChainNFT = ChainEngineSDK.ChainEngineApi.Remote.Models.OnChainNFT;

namespace ChainEngineSDK.ChainEngineApi.Remote.Interfaces
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
        public UniTask<List<OnChainNFT>> GetPlayerNFTs(string gameId);

        /*
         * Developer should be able to fetch an specific NFT.
         */
        public UniTask<OffChainNFT> GetPlayerNFT(string id);

    }
}