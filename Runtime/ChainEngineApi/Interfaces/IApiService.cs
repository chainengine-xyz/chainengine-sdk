using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace ChainEngineSDK.ChainEngineApi.Interfaces
{
    public interface IApiService
    {
        public UniTask<Player> GetByWallet(string wallet);

        public Task<List<RemoteNFT>> GetNFTsByPlayer(string wallet);
    } 
}