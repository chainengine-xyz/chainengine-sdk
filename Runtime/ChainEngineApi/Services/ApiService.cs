using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Datasource;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Shared.Exceptions;
using Cysharp.Threading.Tasks;
using OnChainNFT = ChainEngineSDK.ChainEngineApi.Remote.Models.OnChainNFT;

namespace ChainEngineSDK.ChainEngineApi.Services
{
    public class ApiService : Interfaces.IApiService
    {
        private readonly ApiClient _client;
        private readonly DataSourceApi _dataSource;
        
        public ApiService(ApiClient client)
        {
            _client = client;
            _dataSource = new DataSourceApi(client);
        }

        public UniTask<Player> CreatePlayer(string walletAddress)
        {
            var player = new Player{
                WalletAddress = walletAddress,
                AccountId = _client.getAccountId(),
                GameId = _client.GetGameId()
            };
            
            return _dataSource.CreatePlayer(player);
        }

        public UniTask<Player> GetPlayerInfo()
        {
            return _dataSource.GetPlayerInfo();
        }

        public UniTask<List<OnChainNFT>> GetPlayerNFTs()
        {
            if (_client.GetGameId() == null) throw new UnableToLoadNFTs("game id not set properly");
            
            return _dataSource.GetPlayerNFTs(_client.GetGameId());
        }

        public UniTask<OffChainNFT> GetPlayerNFT(string id)
        {
            return _dataSource.GetPlayerNFT(id);
        }
    }
}