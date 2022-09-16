using ChainEngine.Interfaces;
using ChainEngine.Model;
using ChainEngine.Remote.Datasource;
using ChainEngine.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngine.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly DataSourceApi _dataSource;
        private readonly string _gameId;

        public PlayerService(ChainEngineSDK chainEngineSDK)
        {
            _dataSource = new DataSourceApi(chainEngineSDK);
            _gameId = chainEngineSDK.GameId;
        }

        public UniTask<Player> CreateOrFetch(string walletAddress)
        {
            var player = new NewPlayerRequest{
                WalletAddress = walletAddress,
                GameId = _gameId,
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

        public UniTask<string> TransferNft(string walletAddress, string nftId, int amount)
        {
            var transfer = new SignedTransferRequest{
                WalletAddress = walletAddress,
                NftId = nftId,
                Amount = amount,
            };

            return _dataSource.TransferNft(transfer);
        }

        public UniTask<string> GetNonce()
        {
            return _dataSource.GetNonce();
        }
    }
}
