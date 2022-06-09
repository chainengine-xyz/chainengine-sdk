using ChainEngineSDK.ChainEngineApi.Client;

namespace ChainEngineSDK.ChainEngineApi
{
    public class ChainEngineClient
    {
        readonly ApiClient ApiClient = new ApiClient();
        
        static readonly ChainEngineClient s_instance = new ChainEngineClient();
        
        private ChainEngineClient() { }

        public static void Initialize(string accountId, string gameId)
        {
            s_instance.ApiClient.Initialize(accountId, gameId);
        }

        public static ApiClient Client
        {
            get => s_instance.ApiClient;
        }
    }
}