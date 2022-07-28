using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace ChainEngineSDK.Client
{
    public class ChainEngineWebClient
    {
        private readonly UnityWebRequest _unityWebRequest;

        public ChainEngineWebClient(string playerApiKey, string path, string method, [CanBeNull] string apiMode)
        {
            _unityWebRequest = new UnityWebRequest(path, method);

            if (!string.IsNullOrEmpty(playerApiKey))
            {
                _unityWebRequest.SetRequestHeader("x-api-key", playerApiKey);
            }
            
            if (!string.IsNullOrEmpty(apiMode))
            {
                _unityWebRequest.SetRequestHeader("api-mode", apiMode);
            }
            
            _unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        }

        public async UniTask<UnityWebRequest> SendWebRequest()
        {
            return await _unityWebRequest.SendWebRequest();
        }

        public UploadHandlerRaw uploadHandler
        {
            set => _unityWebRequest.uploadHandler = value;
        }
        
        public DownloadHandlerBuffer downloadHandler
        {
            set => _unityWebRequest.downloadHandler = value;
        }
    }
}