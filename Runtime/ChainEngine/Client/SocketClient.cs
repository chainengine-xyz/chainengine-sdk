using System;
using ChainEngine.Interfaces;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using ChainEngine.Remote.Datasource;
using UnityEngine;

namespace ChainEngine.Client
{
    public static class SocketClient
    {
        public static IWebGLPollingClient Build(ChainEngineSDK client)
        {
            var _socket = new WebGLPollingClient(client);

            return _socket;
        }

        public static SocketIOUnity Build(string _namespace, bool debug = false)
        {
            var uri = new Uri($"{DataSourceApi.ServerURL}/{_namespace}");

            var _socket = new SocketIOUnity(uri, new SocketIOOptions
            {
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
            });

            _socket.JsonSerializer = new NewtonsoftJsonSerializer();

            if (debug)
            {
                _socket.OnConnected += (sender, e) =>
                {
                    Debug.Log($"Socket OnConnected: {e}");
                };

                _socket.OnDisconnected += (sender, e) =>
                {
                    Debug.Log($"Disconnect: {e}");
                };

                _socket.OnReconnectAttempt += (sender, e) =>
                {
                    Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
                };

                Debug.Log("Connecting...");
            }

            _socket.Connect();

            return _socket;
        }
    }
}
