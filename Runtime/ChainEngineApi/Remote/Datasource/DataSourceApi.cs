using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Interfaces;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Shared.Exceptions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using OnChainNFT = ChainEngineSDK.ChainEngineApi.Remote.Models.OnChainNFT;

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

        public async UniTask<List<OnChainNFT>> GetPlayerNFTs(string gameId)
        {
            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/players/nfts?fetchFor=player", "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            var req = await www.SendWebRequest();
            Debug.Log(req.downloadHandler.text);

            var callResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<OnChainNFTCallResponse>(req.downloadHandler.text);

            return callResponse.Items.First().nfts;
        }

        public async UniTask<OffChainNFT> GetPlayerNFT(string id)
        {
            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/players/nfts/" + id, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            var req = await www.SendWebRequest();
            Debug.Log(req.downloadHandler.text);

            var nft = Newtonsoft.Json.JsonConvert.DeserializeObject<OffChainNFT>(req.downloadHandler.text);

            return nft;
        }
    }
    
    
}