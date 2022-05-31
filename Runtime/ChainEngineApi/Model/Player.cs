namespace ChainEngineSDK.ChainEngineApi.Model
{
    public class Player
    {
        private string _id;
        public string WalletAddress;
        public string AccountId;
        public string GameId;

        public string GetId()
        {
            return _id;
        }
    }
}