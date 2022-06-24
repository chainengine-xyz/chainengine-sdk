using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Services;
using Cysharp.Threading.Tasks;

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
        
        public string GetPlayerKey()
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
        
        public async UniTask<PlayerNftCollection> GetPlayerNFTs(int page = 1, int limit = 10)
        {
            var collection = new PlayerNftCollection(limit, page, _apiService);
            return await collection.FirstPage();
        }
        
        public async UniTask<Nft> GetPlayerNFT(string chainId)
        {
            return await _apiService.GetPlayerNFT(chainId);
        }
    }
}