using ChainEngineSDK.ChainEngineApi.Client;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace ChainEngineApi.Client
{
    public class ChainEngineWebClient
    {
        private readonly UnityWebRequest _unityWebRequest;

        public ChainEngineWebClient(ApiClient client, string path, string method)
        {
            _unityWebRequest = new UnityWebRequest(path, method);

            if (!string.IsNullOrEmpty(client.GetPlayerKey()))
            {
                _unityWebRequest.SetRequestHeader("x-api-key", client.GetPlayerKey());
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