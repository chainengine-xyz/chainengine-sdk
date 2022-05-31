using System;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Interfaces;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Services;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace ChainEngineSDK.ChainEngineApi.Client
{
    public class ApiClient
    {
        private string url = "";
        
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

        public string getGameId()
        {
            return _gameId;
        }
        
        public string getApiKey()
        {
            return _apikey;
        }
        
        public string getSecret()
        {
            return _secret;
        }

        public string getAccountId()
        {
            return _accountId;
        }

        
        public async UniTask<Player> CreatePlayer(string walletAddress)
        {
            return await _consoleService.CreatePlayer(walletAddress);
        }
        
        public async Task<Nft> CreateNft(NftMetadata metadata)
        {
            return await CreateNft(metadata, null);
        }
        
        public async Task<Nft> CreateNft(NftMetadata metadata, [CanBeNull] string destination)
        {
            return await _consoleService.CreateNft(metadata);
        }
        
        public async UniTask<string> getNFTsByWallet(string address)
        {
            return await _apiService.getNFTsByPlayer(address);
        }
    }
}