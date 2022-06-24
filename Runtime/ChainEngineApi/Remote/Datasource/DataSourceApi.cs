using System;
using System.Threading.Tasks;
using ChainEngineApi.Client;
using ChainEngineApi.Model;
using ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Remote.Interfaces;
using ChainEngineSDK.ChainEngineApi.Shared.Exceptions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ChainEngineSDK.ChainEngineApi.Remote.Datasource
{
    public class DataSourceApi: IDataSourceApi
    {
        private const string ServerURL = "http://localhost:3000";

        private readonly ApiClient _apiClient;

        public DataSourceApi(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async UniTask<Player> CreatePlayer(Player playerDto)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(playerDto);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/players", "POST")
            {
                uploadHandler = new UploadHandlerRaw(jsonEncoded),
                downloadHandler = new DownloadHandlerBuffer()
            };

            try
            {
                var req = await www.SendWebRequest();
                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Player>(req.downloadHandler.text);
            }
            catch (Exception)
            {
                throw new PlayerNotCreated();
            }
        }

        public async UniTask<Player> GetPlayerInfo()
        {
            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/players", "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            var req = await www.SendWebRequest();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Player>(req.downloadHandler.text);
        }

        public async Task<NftCallResponse> GetPlayerNFTs(int page, int limit)
        {
            var uri = ServerURL + "/players/nfts?queryBy=player&limit=" + limit + "&page=" + page;
            var www = new ChainEngineWebClient(_apiClient, uri, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            var req = await www.SendWebRequest();

            NftCallResponse callResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<NftCallResponse>(req.downloadHandler.text);

            return callResponse;
        }

        public async UniTask<Nft> GetPlayerNFT(string id)
        {
            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/players/nfts/" + id, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            var req = await www.SendWebRequest();
            Debug.Log(req.downloadHandler.text);

            var nft = Newtonsoft.Json.JsonConvert.DeserializeObject<Nft>(req.downloadHandler.text);

            return nft;
        }
    }
    
    
}