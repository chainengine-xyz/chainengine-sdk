using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace ChainEngine.Client
{
    public class ChainEngineWebClient
    {
        private readonly UnityWebRequest _unityWebRequest;

        public ChainEngineWebClient(string token, string path, string method, [CanBeNull] string apiMode)
        {
            _unityWebRequest = new UnityWebRequest(path, method);

            if (!string.IsNullOrEmpty(token))
            {
                _unityWebRequest.SetRequestHeader("Authorization", $"Bearer {token}");
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
