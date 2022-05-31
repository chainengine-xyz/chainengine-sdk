using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Interfaces;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Datasource;
using Cysharp.Threading.Tasks;
using PlasticGui.Configuration.CloudEdition.Welcome;

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
                walletAddress = walletAddress,
                accountId = _client.getAccountId(),
                gameId = _client.getGameId()
            };
            
            var remote = new DataSourceApi(_client);
            return await remote.CreatePlayer(player);
        }

        public async UniTask<Nft> CreateNft(NftMetadata metadata)
        {
            var nft = new Nft
            {
                name = metadata.name,
                description = metadata.description,
                imageURI = metadata.imageURI,
                gameId = _client.getGameId()
            };
            
            var remote = new DataSourceApi(_client);
            return await remote.CreateNft(nft);
        }
    }
}