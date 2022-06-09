using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using OnChainNFT = ChainEngineSDK.ChainEngineApi.Remote.Models.OnChainNFT;

namespace ChainEngineSDK.ChainEngineApi.Interfaces
{
    public interface IApiService
    {
        /*
         * Game developer should be able to create a player. 
         */
        public UniTask<Player> CreatePlayer(string walletAddress);
        
        /*
         * Game developer should be able to fetch players info.
         */
        public UniTask<Player> GetPlayerInfo();
        
        /*
         * Game developer should be able to fetch player's NFTs.
         */
        public UniTask<List<OnChainNFT>> GetPlayerNFTs();

        /*
         * Developer should be able to fetch an specific NFT.
         */
        public UniTask<OffChainNFT> GetPlayerNFT(string chainId);
    } 
}