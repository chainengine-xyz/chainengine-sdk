using ChainEngineSDK.ChainEngineApi.Client;

namespace ChainEngineSDK.ChainEngineApi
{
    public class ChainEngineClient
    {
        ApiClient apiClient = new ApiClient();
        
        static ChainEngineClient instance = new ChainEngineClient();
        
        private ChainEngineClient() { }

        public static void Initialize(string accountId, string gameId, string apikey, string secret)
        {
            instance.apiClient.Initialize(accountId, gameId, apikey, secret);
        }

        public static ApiClient Client
        {
            get => instance.apiClient;
        }
    }
}