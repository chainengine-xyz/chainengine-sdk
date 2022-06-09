using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Services;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using OnChainNFT = ChainEngineSDK.ChainEngineApi.Remote.Models.OnChainNFT;

namespace ChainEngineSDK.ChainEngineApi.Client
{
    public class ApiClient
    {
        private string _playerKey;
        private string _gameId;
        private string _accountId;

        private readonly ApiService _apiService;

        public ApiClient()
        {
            _apiService = new ApiService(this);
        }

        public void Initialize(string accountId, string gameId)
        {
            _gameId = gameId;
            _accountId = accountId;
        }

        public string getAccountId()
        {
            return _accountId;
        }

        public void setPlayerKey(string playerKey)
        {
            _playerKey = playerKey;
        }
        
        public string getPlayerKey()
        {
            return _playerKey;
        }

        public string GetGameId()
        {
            return _gameId;
        }
        
        public async UniTask<Player> CreatePlayer(string walletAddress)
        {
            return await _apiService.CreatePlayer(walletAddress);
        }

        public async UniTask<Player> GetPlayerInfo()
        {
            return await _apiService.GetPlayerInfo();
        }
        
        public async Task<List<OnChainNFT>> GetPlayerNFTs()
        {
            return await _apiService.GetPlayerNFTs();
        }
        
        public async UniTask<OffChainNFT> GetPlayerNFT(string chainId)
        {
            return await _apiService.GetPlayerNFT(chainId);
        }
    }
}