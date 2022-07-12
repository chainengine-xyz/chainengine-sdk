using ChainEngineSDK.Model;
using ChainEngineSDK.Services;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK
{
    public class ChainEngineClient
    {
        private string _playerKey;
        private string _gameId;
        private string _accountId;

        private readonly ApiService _apiService;

        public ChainEngineClient(string accountId, string gameId)
        {
            _apiService = new ApiService(this);
            _gameId = gameId;
            _accountId = accountId;
        }

        public string getAccountId()
        {
            return _accountId;
        }

        public void setPlayerKey(string playerKey)
        {
            _playerKey = playerKey;
        }
        
        public string GetPlayerKey()
        {
            return _playerKey;
        }

        public string GetGameId()
        {
            return _gameId;
        }
        
        public async UniTask<Player> CreatePlayer(string walletAddress)
        {
            return await _apiService.CreatePlayer(walletAddress);
        }

        public async UniTask<Player> GetPlayerInfo()
        {
            return await _apiService.GetPlayerInfo();
        }
        
        public async UniTask<PlayerNftCollection> GetPlayerNFTs(int page = 1, int limit = 10)
        {
            var collection = new PlayerNftCollection(limit, page, _apiService);
            return await collection.FirstPage();
        }
        
        public async UniTask<Nft> GetPlayerNFT(string id)
        {
            return await _apiService.GetPlayerNFT(id);
        }
    }
}