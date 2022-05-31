using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Remote.Datasource;
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

        public async UniTask<string> getByWallet(string name)
        {
            var remote = new DataSourceApi(_client);
            return await remote.GetPlayerByWallet("12345");
        }

        public async UniTask<string> getNFTsByPlayer(string wallet)
        {
            var remote = new DataSourceApi(_client);
            return await remote.GetNFTsByPlayer(wallet);
        }
    }
}