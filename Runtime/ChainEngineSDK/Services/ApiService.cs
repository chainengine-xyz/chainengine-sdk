using System.Threading.Tasks;
using ChainEngineSDK.Interfaces;
using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Datasource;
using ChainEngineSDK.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.Services
{
    public class ApiService : IApiService
    {
        private readonly ChainEngineClient _chainEngineClient;
        private readonly DataSourceApi _dataSource;
        
        public ApiService(ChainEngineClient chainEngineClient)
        {
            _chainEngineClient = chainEngineClient;
            _dataSource = new DataSourceApi(chainEngineClient);
        }

        public UniTask<Player> CreatePlayer(string walletAddress)
        {
            var player = new Player{
                WalletAddress = walletAddress,
                AccountId = _chainEngineClient.getAccountId(),
                GameId = _chainEngineClient.GetGameId()
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