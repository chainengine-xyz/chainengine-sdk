using System;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using ChainEngineSDK.Remote.Datasource;

namespace ChainEngineSDK.Client
{
    public static class SocketClient
    {
        public static SocketIOUnity Build(string _namespace)
        {
            var uri = new Uri($"{DataSourceApi.ServerURL}/${_namespace}");
            
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