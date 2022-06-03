using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChainEngineSDK.ChainEngineApi.Client
{
    public class Web3Client
    {
        [DllImport("__Internal")]
        private static extern void Web3Connect();

        [DllImport("__Internal")]
        private static extern string ConnectAccount();

        [DllImport("__Internal")]
        private static extern void SetConnectAccount(string value);

        private int expirationTime;
        private string account; 

        public UniTask<string> Connect()
        {
            Web3Connect();
            return OnConnected();
        }

        private async UniTask<string> OnConnected()
        {
            account = ConnectAccount();
            
            while (account == "") {
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                account = ConnectAccount();
            };
            
            SetConnectAccount(account);
            
            return account;
        }
    }
}
