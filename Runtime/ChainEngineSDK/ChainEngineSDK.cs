using System;
using System.Collections.Generic;
using System.Text.Json;
using ChainEngineSDK.Actions;
using ChainEngineSDK.Client;
using Cysharp.Threading.Tasks;
using ChainEngineSDK.Services;
using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Datasource;
using UnityEngine;

namespace ChainEngineSDK
{
    public class ChainEngineClient : MonoBehaviour
    {
        private const string ACCOUNT_MODE_TEST = "testnet";
        private const string ACCOUNT_MODE_PROD = "mainnet";
        
        private static ChainEngineClient _instance;
        private PlayerService _playerService;
        private SocketIOUnity _socketClient;
        private bool _isProdMode;
        private Player _player;

        [SerializeField]
        private string _gameId;
        
        private static bool Exists() {
            return _instance != null;
        }

        private static bool VerifyGameId()
        {
            return _instance != null && _instance.GameId != "";
        }
        
        void Awake() {
            if (_instance == null) {
                Application.runInBackground = true;
                
                _instance = this;
            
                _socketClient = SocketClient.Build("metamask");
                _playerService = new PlayerService(_instance);
                
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void OnDestroy() {
            _instance = null;
        }

        public static ChainEngineClient Instance()
        {
            if (!Exists ()) {
                throw new Exception ("ChainEngineClient could not find the ChainEngineSDK object. Please ensure you have added the ChainEngineSDK Prefab to your scene.");
            }
            
            if (!VerifyGameId ()) {
                throw new Exception ("ChainEngineClient could not find the Game Id value. Please ensure you have setup the Game Id value on the ChainEngineSDK Prefab on your scene.");
            }
            
            return _instance;
        }

        public string PlayerKey => _player?.APIKey;

        public string GameId => _gameId;

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

        public void MetamaskLogin()
        {
            var nonce = Guid.NewGuid().ToString();
            
            _socketClient.On($"metamask/{nonce}", (response) =>
            {
                if (response.Count != 1) return;
                
                var data = response.GetValue().Deserialize<Dictionary<string, string>>();

                if (data!.ContainsKey("error"))
                {
                    Debug.Log(data!["error"]);
                } else if (data!.ContainsKey("walletAddress"))
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        MetamaskDispatcher(data!["walletAddress"]);
                    });
                }
                
                _socketClient.Off($"metamask/{nonce}");
            });

#if UNITY_WEBGL
            /*
             * TODO
             * integrate with the web3 inside the webgl player
             */
#else
            Application.OpenURL(GetApplicationUri(nonce));
#endif
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

        private async void MetamaskDispatcher(string walletAddress)
        {
            _player = await _playerService.CreateOrFetch(walletAddress);
                    
            ChainEngineActions.OnReceiveMetamaskPlayer?.Invoke(_player);
        }
        
        private string GetApplicationUri(string nonce)
        {
            var uri = new Uri($"{DataSourceApi.UiURL}/metamask/{nonce}");
            
#if UNITY_ANDROID || UNITY_IOS
            uri = new Uri($"https://metamask.app.link/dapp/{uri.Host + uri.Port + uri.PathAndQuery}");
#endif

            return uri.ToString();
        }
    }
}