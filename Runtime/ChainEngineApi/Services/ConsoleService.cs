using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Interfaces;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Datasource;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.ChainEngineApi.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly ApiClient _client;

        public ConsoleService(ApiClient client)
        {
            _client = client;
        }
        
        public async UniTask<Player> CreatePlayer(string walletAddress)
        {
            var player = new Player{
                WalletAddress = walletAddress,
                AccountId = _client.GetAccountId(),
                GameId = _client.GetGameId()
            };
            
            var remote = new DataSourceApi(_client);
            return await remote.CreatePlayer(player);
        }
        
        public async UniTask<Player> GetPlayerByWallet(string walletAddress)
        {
            var player = new Player{
                WalletAddress = walletAddress,
                AccountId = _client.GetAccountId(),
                GameId = _client.GetGameId()
            };
            
            var remote = new DataSourceApi(_client);
            return await remote.CreatePlayer(player);
        }

        public async UniTask<Nft> MintNFT(NftMetadata metadata)
        {
            var nft = new Nft
            {
                Name = metadata.Name,
                Description = metadata.Description,
                ImageUri = metadata.ImageUri,
                GameId = _client.GetGameId()
            };
            
            var remote = new DataSourceApi(_client);
            return await remote.MintNFT(nft);
        }
    }
}