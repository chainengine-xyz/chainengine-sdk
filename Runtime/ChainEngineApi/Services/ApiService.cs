using System.Threading.Tasks;
using ChainEngineApi.Model;
using ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Remote.Datasource;
using Cysharp.Threading.Tasks;

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

        public async Task<NftCallResponse> GetPlayerNFTs(int page, int limit)
        {
            return await _dataSource.GetPlayerNFTs(page, limit);
        }

        public UniTask<Nft> GetPlayerNFT(string id)
        {
            return _dataSource.GetPlayerNFT(id);
        }
    }
}