using ChainEngine.Remote.Datasource;
using ChainEngine.Shared.Exceptions;
using ChainEngine.Remote.Models;
using ChainEngine.Interfaces;
using ChainEngine.Services;
using ChainEngine.Actions;
using ChainEngine.Client;
using ChainEngine.Model;
using ChainEngine.Types;
using Cysharp.Threading.Tasks;
using SocketIOClient;
using UnityEngine;
using System.Collections;
using System;
using JWT;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ChainEngine
{
    public class ChainEngineSDK : MonoBehaviour
    {
        private const string ACCOUNT_MODE_TEST = "testnet";
        private const string ACCOUNT_MODE_PROD = "mainnet";

        private static ChainEngineSDK _instance;
        private PlayerService _playerService;
#if UNITY_WEBGL && !UNITY_EDITOR
        private IWebGLPollingClient _socketClient;
#else
        private SocketIOUnity _socketClient;
#endif
        private bool _alreadyWarned;
        private bool _isProdMode;

        [SerializeField]
        private string _gameId;

        void Awake()
        {
            if (_instance != null) return;
            
            Application.runInBackground = true;

            _instance = this;
#if UNITY_WEBGL && !UNITY_EDITOR
            _socketClient = SocketClient.Build(_instance);
#else
            _socketClient = SocketClient.Build("wallet");
#endif
            _playerService = new PlayerService(_instance);

            DontDestroyOnLoad(this.gameObject);
        }

        void OnDestroy() {
            _instance = null;
        }

        public static ChainEngineSDK Instance()
        {
            if (!Exists ()) {
                throw new Exception ("ChainEngineSDK could not find the ChainEngine object. Please ensure you have added the ChainEngine Prefab to your scene.");
            }

            if (!VerifyGameId ()) {
                throw new Exception ("ChainEngineSDK could not find the Game Id value. Please ensure you have setup the Game Id value on the ChainEngine Prefab on your scene.");
            }

            return _instance;
        }

        public string ApiMode => _isProdMode ? ACCOUNT_MODE_PROD : ACCOUNT_MODE_TEST;

        public string GameId => _gameId;
        
        public Player Player { get; private set; }

        public bool IsPlayerAuthenticated => Player != null && ValidateToken(Player!.Token) != null;

        public void SetTestNetMode()
        {
            _isProdMode = false;
        }
        
        public void SetMainNetMode()
        {
            _isProdMode = true;
        }

        public async UniTask<Player> CreateOrFetchPlayer(string walletAddress)
        {
            TestNetWarning();
            Player = await _playerService.CreateOrFetch(walletAddress);

            return Player;
        }

        public async void PlayerAuthentication(WalletProvider wallet = WalletProvider.Browser)
        {
            TestNetWarning();
            var nonce = await _playerService.GetNonce();
#if UNITY_WEBGL && !UNITY_EDITOR
            _socketClient.PollingOnUnityThread($"login/{nonce}", (response) =>
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthSocket>(response);
                AuthHandler(nonce, data);
            });
#else
            _socketClient.OnUnityThread($"login/{nonce}", (response) =>
            {
                var data = response.GetValue<AuthSocket>();
                AuthHandler(nonce, data);
            });
#endif

            Application.OpenURL(GetAuthenticationUri(nonce, wallet));
        }

        public void PlayerLogout()
        {
            if (!IsPlayerAuthenticated) return;

            Player = null;
        }

        public async void TransferNft(string walletAddress, string nftId, int amount)
        {
            TestNetWarning();
            var transferId = await _playerService.TransferNft(walletAddress, nftId, amount);
            
            Application.OpenURL(GetTransferSignatureUri(transferId));
        }

        public async UniTask<PlayerNftCollection> GetPlayerNFTs(int page = 1, int limit = 10)
        {
            TestNetWarning();
            var collection = new PlayerNftCollection(limit, page, _playerService);
            return await collection.FirstPage();
        }

        public async UniTask<Nft> GetNFT(string id)
        {
            TestNetWarning();
            return await _playerService.GetNFT(id);
        }

        private void TestNetWarning()
        {
            if (_isProdMode || _alreadyWarned) return;
            
            _alreadyWarned = true;
            Debug.LogWarning("ChainEngine's SDK is running on TestNet mode, make sure to switch over MainNet for production builds.");
        }

        private void AuthHandler(string nonce, AuthSocket data)
        {
            if (data!.Error != null)
            {
                ChainEngineActions.OnAuthFailure?.Invoke(new AuthError(data.Error));
            } else if (data!.Token != null)
            {
                AuthDispatcher(data.Token);
            }

            _socketClient.Off($"login/{nonce}");
        }

        private Token ValidateToken(string jwt)
        {
            if (string.IsNullOrEmpty(jwt)) return null;
            
            var payload = JsonWebToken.Decode(jwt, String.Empty, false);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(payload);

            return data;
        }

        private void AuthDispatcher(string jwt)
        {
            var token = ValidateToken(jwt);

            if (token == null)
            {
                ChainEngineActions.OnAuthFailure?.Invoke(new AuthError("Unable to decode player token"));
                return;
            }
            
            Player = new Player{
                WalletAddress = token.WalletAddress,
                Id = token.Id,
                Token = jwt,
            };
            
            ChainEngineActions.OnAuthSuccess?.Invoke(Player);
        }

        private string GetAuthenticationUri(string nonce, WalletProvider wallet)
        {
            var uri = new Uri($"{DataSourceApi.UiURL}/game-authentication/{nonce}");

#if UNITY_ANDROID || UNITY_IOS
            switch(wallet)
            {
                case WalletProvider.TrustWallet:
                    var polygonChainId = 137;

                    uri = new Uri($"https://link.trustwallet.com/open_url?coin_id={polygonChainId}&url={uri}");

                    break;
                case WalletProvider.Metamask:
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

                    break;
                case WalletProvider.Coinbase:
#if UNITY_ANDROID
                    uri = new Uri($"https://go.cb-w.com/dapp?cb_url={UnityWebRequest.EscapeURL(uri.ToString())}");
#else
                    uri = new Uri($"cbwallet://dapp?url={UnityWebRequest.EscapeURL(uri.ToString())}");
#endif
                    break;
                default:
                    Debug.LogWarning("On mobile builds you may want to specify a WalletProvider so the player can authenticate using an specific wallet app.");
                    break;
            }
#endif

            return uri.ToString();
        }
        
        private string GetTransferSignatureUri(string transferId)
        {
            var uri = new Uri($"{DataSourceApi.UiURL}/sign-transaction/{transferId}");

            return uri.ToString();
        }

        private static bool Exists() {
            return _instance != null;
        }

        private static bool VerifyGameId()
        {
            return !string.IsNullOrEmpty(_instance!.GameId);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UnityEditor.SceneManagement.PrefabStage prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            bool isValidPrefabStage = prefabStage != null && prefabStage.stageHandle.IsValid();
            bool prefabConnected = PrefabUtility.GetPrefabInstanceStatus(this.gameObject) == PrefabInstanceStatus.Connected;

            if (!isValidPrefabStage && prefabConnected && string.IsNullOrWhiteSpace(GameId))
            {
                Debug.LogError("Setup your ChainEngine Game Id before running.");
            }
        }
#endif

        public void Coroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
