using ChainEngineSDK.ChainEngineApi.Client;

namespace ChainEngineSDK.ChainEngineApi
{
    public class ChainEngineClient
    {
        private readonly ApiClient _apiClient = new ApiClient();

        private static readonly ChainEngineClient SInstance = new ChainEngineClient();
        
        private ChainEngineClient() {}

        public static void Initialize(string accountId, string gameId)
        {
            SInstance._apiClient.Initialize(accountId, gameId);
        }

        public static ApiClient Client => SInstance._apiClient;
    }
}