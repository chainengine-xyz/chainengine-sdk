using System;
using ChainEngineSDK.Actions;
using ChainEngineSDK.Client;
using Cysharp.Threading.Tasks;
using ChainEngineSDK.Services;
using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Datasource;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

        void Awake() {
            if (_instance == null) {
                _instance = this;
            
                _socketClient = SocketClient.Build("wallet");
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

        public async void WalletLogin()
        {
            var nonce = await _playerService.GetNonce();
            
            _socketClient.OnUnityThread($"login/{nonce}", (response) =>
            {
                var data = response.GetValue<AuthSocket>();
                
                if (data!.Error != null)
                {
                    Debug.Log(data.Error);
                } else if (data!.WalletAddress != null)
                {
                    WalletDispatcher(data.WalletAddress);
                }
                
                _socketClient.Off($"login/{nonce}");
            });

#if UNITY_WEBGL
            /*
             * TODO
             * integrate with the web3 inside the webgl player ?
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

        private async void WalletDispatcher(string walletAddress)
        {
            _player = await _playerService.CreateOrFetch(walletAddress);
                    
            ChainEngineActions.OnReceiveMetamaskPlayer?.Invoke(_player);
        }
        
        private string GetApplicationUri(string nonce)
        {
            var uri = new Uri($"{DataSourceApi.UiURL}/wallet-authentication/{nonce}");
            
#if UNITY_ANDROID || UNITY_IOS
            /*
             * MetaMask Deep Link is broken :/
             * https://github.com/MetaMask/metamask-mobile/issues/3855
             */
            // uri = new Uri($"https://metamask.app.link/dapp/{uri.Host + uri.PathAndQuery}");
            
            /*
             * Workaround
             * https://github.com/MetaMask/metamask-mobile/issues/3965#issuecomment-1122505112
             */
            uri = new Uri($"dapp://{uri.Host + uri.PathAndQuery}");
#endif

            return uri.ToString();
        }
        
        private static bool Exists() {
            return _instance != null;
        }

        private static bool VerifyGameId()
        {
            return _instance!.GameId != "";
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            UnityEditor.SceneManagement.PrefabStage prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            bool isValidPrefabStage = prefabStage != null && prefabStage.stageHandle.IsValid();
            bool prefabConnected = PrefabUtility.GetPrefabInstanceStatus(this.gameObject) == PrefabInstanceStatus.Connected;
            
            if (!isValidPrefabStage && prefabConnected && string.IsNullOrWhiteSpace(GameId))
            {
                Debug.LogError("Setup your ChainEngineSDK Game Id before running.");
            }
        }
#endif
    }
}