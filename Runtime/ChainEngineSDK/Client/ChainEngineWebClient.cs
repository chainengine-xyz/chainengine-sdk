using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace ChainEngineSDK.Client
{
    public class ChainEngineWebClient
    {
        private readonly UnityWebRequest _unityWebRequest;

        public ChainEngineWebClient(ChainEngineClient client, string path, string method)
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