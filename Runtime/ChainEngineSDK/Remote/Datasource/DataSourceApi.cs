using System;
using System.Threading.Tasks;
using ChainEngineSDK.Client;
using ChainEngineSDK.Model;
using ChainEngineSDK.Remote.Interfaces;
using ChainEngineSDK.Remote.Models;
using ChainEngineSDK.Shared.Exceptions;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace ChainEngineSDK.Remote.Datasource
{
    public class DataSourceApi: IDataSourceApi
    {
        //private const string ServerURL = "https://api.chainengine.xyz";
        private const string ServerURL = "http://localhost:3000";

        private readonly ChainEngineClient _apiClient;

        public DataSourceApi(ChainEngineClient client)
        {
            _apiClient = client;
        }
        
        public async UniTask<Player> CreatePlayer(Player playerDto)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(playerDto);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/clientapp/players", "POST")
            {
                uploadHandler = new UploadHandlerRaw(jsonEncoded),
                downloadHandler = new DownloadHandlerBuffer()
            };

            try
            {
                var req = await www.SendWebRequest();
                
                var newPlayer = Newtonsoft.Json.JsonConvert.DeserializeObject<NewPlayerResponse>(req.downloadHandler.text);
                if(newPlayer == null) throw new Exception("Invalid response from api");

                return newPlayer.player;
            }
            catch (Exception exception)
            {
                throw new PlayerNotCreated();
            }
        }

        public async UniTask<Player> GetPlayerInfo()
        {
            var www = new ChainEngineWebClient(_apiClient, ServerURL + "/clientapp/players", "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            var req = await www.SendWebRequest();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Player>(req.downloadHandler.text);
        }

        public async Task<NftCallResponse> GetPlayerNFTs(int page, int limit)
        {
            try
            {
                var uri = ServerURL + "/clientapp/players/nfts?queryBy=player&limit=" + limit + "&page=" + page;
                var www = new ChainEngineWebClient(_apiClient, uri, "GET")
                {
                    downloadHandler = new DownloadHandlerBuffer()
                };

                var req = await www.SendWebRequest();

                NftCallResponse callResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<NftCallResponse>(req.downloadHandler.text);

                return callResponse;
            } catch (Exception exception)
            {
                throw new UnableToLoadNFTs();
            }
        }

        public async UniTask<Nft> GetPlayerNFT(string id)
        {
            try
            {
                var www = new ChainEngineWebClient(_apiClient, ServerURL + "/clientapp/players/nfts/" + id, "GET")
                {
                    downloadHandler = new DownloadHandlerBuffer()
                };

                var req = await www.SendWebRequest();

                var nft = Newtonsoft.Json.JsonConvert.DeserializeObject<Nft>(req.downloadHandler.text);

                return nft;
            }
            catch (Exception exception)
            {
                throw new UnableToLoadNFTs();
            }
        }
    }
    
    
}