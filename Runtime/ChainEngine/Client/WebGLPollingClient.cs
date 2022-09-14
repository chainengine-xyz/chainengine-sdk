using ChainEngine.Remote.Datasource;
using ChainEngine.Remote.Models;
using ChainEngine.Interfaces;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine;

namespace ChainEngine.Client
{
    public class WebGLPollingClient : IWebGLPollingClient
    {
        private enum UnityThreadScope
        {
            Update,
            LateUpdate,
            FixedUpdate
        }

        private readonly UnityThreadScope _unityThreadScope = UnityThreadScope.Update;
        private readonly Dictionary<string, Action<string>> _eventHandlers;
        private readonly UnityWebRequest _unityWebRequest;
        private readonly ChainEngineSDK _client;

        public WebGLPollingClient(ChainEngineSDK client)
        {
            UnityThread.initUnityThread();
            _eventHandlers = new Dictionary<string, Action<string>>();
            _client = client;
        }


        public void PollingOnUnityThread(string eventName, Action<string> callback)
        {
            if (_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers.Remove(eventName);
            }
            _eventHandlers.Add(eventName, callback);
            
            _client.Coroutine(PollingIterator());
        }

        public void Off(string eventName)
        {
            if (_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers.Remove(eventName);
            }
        }

        private IEnumerator PollingIterator()
        {
            if (_eventHandlers.Count <= 0) yield break;
            
            foreach (KeyValuePair<string, Action<string>> entry in _eventHandlers)
            {
                HandlePolling(entry.Key, entry.Value);
            }
            
            //Wait for 5 seconds
            yield return new WaitForSeconds(5);

            yield return PollingIterator();
        }

        private async void HandlePolling(string eventName, Action<string> callback)
        {
            var nonce = eventName.Replace("login/", "");
            var req = await SendRequest(nonce);

            if (string.IsNullOrEmpty(req.downloadHandler.text)) return;
            
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<SignatureResponse>(req.downloadHandler.text);

            if (data?.Token != null || data?.Token != null)
            {
                ExecuteInUnityThread(() => callback(req.downloadHandler.text));
                _eventHandlers.Remove(eventName);
            }
        }
        
        private void ExecuteInUnityThread(Action action)
        {
            switch (_unityThreadScope)
            {
                case UnityThreadScope.LateUpdate :
                    UnityThread.executeInLateUpdate(action);
                    break;
                case UnityThreadScope.FixedUpdate :
                    UnityThread.executeInFixedUpdate(action);
                    break;
                default :
                    UnityThread.executeInUpdate(action);
                    break;
            }
        }

        private async UniTask<UnityWebRequest> SendRequest(string nonce)
        {
            var uri = new Uri($"{DataSourceApi.ServerURL}/clientapp/auth/{nonce}/polling");
            var www = new UnityWebRequest(uri, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            return await www.SendWebRequest();
        }
    }
}