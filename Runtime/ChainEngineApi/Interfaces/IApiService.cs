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
        [CanBeNull] public UniTask<string> getByWallet(string name);

        public Task<List<RemoteNFT>> getNFTsByPlayer(string wallet);
    } 
}