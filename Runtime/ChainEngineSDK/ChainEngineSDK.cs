using System;
using Cysharp.Threading.Tasks;
using ChainEngineSDK.Services;
using ChainEngineSDK.Model;

namespace ChainEngineSDK
{
    public class ChainEngineClient
    {
        private const string ACCOUNT_MODE_TEST = "testnet";
        private const string ACCOUNT_MODE_PROD = "mainnet";
        private readonly PlayerService _playerService;
        private bool _isProdMode;
        private Player _player;

        public ChainEngineClient(string gameId)
        {
            _playerService = new PlayerService(this);
            GameId = gameId;
        }

        public string PlayerKey => _player?.APIKey;

        public string GameId { get; }

        public string ApiMode => _isProdMode ? ACCOUNT_MODE_PROD : ACCOUNT_MODE_TEST;

        public void SetApiMode(bool mode)
        {
            _isProdMode = mode;
        }
        
        public bool SwitchApiMode()
        {
            _isProdMode = !_isProdMode;

            return _isProdMode;
        }

        public async UniTask<Player> CreateOrFetchPlayer(string walletAddress)
        {
            _player = await _playerService.CreateOrFetch(walletAddress);
            
            return _player;
        }

        public async UniTask<PlayerNftCollection> GetPlayerNFTs(int page = 1, int limit = 10)
        {
            var collection = new PlayerNftCollection(limit, page, _playerService);
            return await collection.FirstPage();
        }
        
        public async UniTask<Nft> GetNFT(string id)
        {
            return await _playerService.GetNFT(id);
        }
    }
}