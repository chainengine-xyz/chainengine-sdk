using ChainEngineSDK.Interfaces;
using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Datasource;
using ChainEngineSDK.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ChainEngineClient _chainEngineClient;
        private readonly DataSourceApi _dataSource;
        
        public PlayerService(ChainEngineClient chainEngineClient)
        {
            _chainEngineClient = chainEngineClient;
            _dataSource = new DataSourceApi(_chainEngineClient);
        }
        
        public UniTask<Player> CreateOrFetch(string walletAddress)
        {
            var player = new Player{
                GameId = _chainEngineClient.GameId,
                WalletAddress = walletAddress,
            };
            
            return _dataSource.CreateOrFetchPlayer(player);
        }

        public async UniTask<NftCallResponse> GetNFTs(int page, int limit)
        {
            return await _dataSource.GetPlayerNFTs(page, limit);
        }

        public UniTask<Nft> GetNFT(string id)
        {
            return _dataSource.GetPlayerNFT(id);
        }
        
        public UniTask<string> GetNonce()
        {
            return _dataSource.GetNonce();
        }
    }
}