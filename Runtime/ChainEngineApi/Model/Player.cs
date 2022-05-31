using System.Collections;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;

namespace ChainEngineSDK.ChainEngineApi.Model
{
    public class Player
    {
        private string id;
        public string walletAddress;
        public string accountId;
        public string gameId;

        public string getId()
        {
            return id;
        }
    }
}