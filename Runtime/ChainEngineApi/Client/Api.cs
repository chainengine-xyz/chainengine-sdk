using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Services;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace ChainEngineSDK.ChainEngineApi.Client
{
    public class ApiClient
    {
        private string _apikey;
        private string _secret;
        private string _gameId;
        private string _accountId;

        private ApiService _apiService;
        private ConsoleService _consoleService;

        public ApiClient()
        {
            _apiService = new ApiService(this);
            _consoleService = new ConsoleService(this);
        }
        
        public void Initialize(string accountId, string gameId, string apikey, string secret)
        {
            _apikey = apikey;
            _secret = secret;
            _gameId = gameId;
            _accountId = accountId;
        }

        public string GetGameId()
        {
            return _gameId;
        }
        
        public string GetApiKey()
        {
            return _apikey;
        }
        
        public string GetSecret()
        {
            return _secret;
        }

        public string GetAccountId()
        {
            return _accountId;
        }

        
        public async UniTask<Player> CreatePlayer(string walletAddress)
        {
            return await _consoleService.CreatePlayer(walletAddress);
        }

        public async UniTask<Player> GetPlayerByWallet(string walletAddress)
        {
            return await _consoleService.CreatePlayer(walletAddress);
        }
        
        public async Task<Nft> MintNFT(NftMetadata metadata) => await MintNFT(metadata, null);

        public async Task<Nft> MintNFT(NftMetadata metadata, [CanBeNull] string destination)
        {
            return await _consoleService.MintNFT(metadata);
        }
        
        public async Task<List<RemoteNFT>> GetNFTsByWallet(string address)
        {
            return await _apiService.GetNFTsByPlayer(address);
        }
    }
}