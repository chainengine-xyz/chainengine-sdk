using ChainEngine.Interfaces;
using ChainEngine.Model;
using ChainEngine.Remote.Datasource;
using ChainEngine.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngine.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ChainEngineSDK _ChainEngineSDK;
        private readonly DataSourceApi _dataSource;

        public PlayerService(ChainEngineSDK ChainEngineSDK)
        {
            _ChainEngineSDK = ChainEngineSDK;
            _dataSource = new DataSourceApi(_ChainEngineSDK);
        }

        public UniTask<Player> CreateOrFetch(string walletAddress)
        {
            var player = new Player{
                GameId = _ChainEngineSDK.GameId,
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
