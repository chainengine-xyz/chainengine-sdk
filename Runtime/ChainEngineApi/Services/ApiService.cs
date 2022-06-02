using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Datasource;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.ChainEngineApi.Services
{
    public class ApiService : Interfaces.IApiService
    {
        private ApiClient _client;
        
        public ApiService(ApiClient client)
        {
            _client = client; 
        }

        public async UniTask<Player> GetByWallet(string wallet)
        {
            var remote = new DataSourceApi(_client);
            return await remote.GetPlayerByWallet(wallet);
        }

        public async Task<List<RemoteNFT>> GetNFTsByPlayer(string wallet)
        {
            var remote = new DataSourceApi(_client);
            return await remote.GetNFTsByPlayer(wallet);
        }
    }
}