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

        public static SocketIOUnity Build(string _namespace)
        {
            var uri = new Uri($"{DataSourceApi.ServerURL}/{_namespace}");

            var _socket = new SocketIOUnity(uri, new SocketIOOptions
            {
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
            });

            _socket.JsonSerializer = new NewtonsoftJsonSerializer();

            _socket.Connect();

            return _socket;
        }
    }
}
