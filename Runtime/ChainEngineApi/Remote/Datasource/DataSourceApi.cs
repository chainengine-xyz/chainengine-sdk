using System;
using System.Collections.Generic;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ChainEngineSDK.ChainEngineApi.Remote.Datasource
{
    public class DataSourceApi: IDataSourceApi
    {
        private string _SERVER_URL = "http://localhost:3000/";
        
        private ApiClient _client;

        public DataSourceApi(ApiClient client)
        {
            _client = client;
        }

        public async UniTask<string> GetPlayerByWallet(string wallet)
        {
            // Prepare object
            var account = new CreatePlayerDto
            {
                email = "nilto1n@chainengine.xyz",
                password = "123456"
            };

            // Prepare JSON
            var accountJson = JsonUtility.ToJson(account);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(accountJson);

            // Prepare uri
            var www = new UnityWebRequest(_SERVER_URL + "accounts/create", "POST");
        
            // Prepare header
            www.SetRequestHeader("x-api-key", _client.getApiKey());
            www.SetRequestHeader("x-api-secret", _client.getSecret());
            www.SetRequestHeader("Content-Type", "application/json");
        
            // Prepare data to upload
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonEncoded);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
            }

            return "Success!";
        }

        public async UniTask<string> GetNFTsByPlayer(string wallet)
        {
            var www = new UnityWebRequest(_SERVER_URL + "accounts/" + _client.getGameId() + "/nfts?fetchFor=player&playerId=test", "GET");
            
            preflightHeader(www);

            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
            }

            return "NFTS";
        }

        public async UniTask<Player> CreatePlayer(Player playerDto)
        {
            var json = JsonUtility.ToJson(playerDto);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

            var www = new UnityWebRequest(_SERVER_URL + "accounts/" + _client.getAccountId() + "/players", "POST");
        
            preflightHeader(www);
        
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonEncoded);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
            }

            return playerDto;
        }

        public async UniTask<Nft> CreateNft(Nft nft)
        {
            var listOfNfts = new List<Nft> { nft };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(listOfNfts);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

            var www = new UnityWebRequest(
                _SERVER_URL + "accounts/" + _client.getAccountId() + "/nfts/game/" + nft.gameId, 
                "POST");

            preflightHeader(www);
        
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonEncoded);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
            }

            return nft;
        }
        
        private void preflightHeader(UnityWebRequest request) {
            request.SetRequestHeader("x-api-key", _client.getApiKey());
            request.SetRequestHeader("x-api-secret", _client.getSecret());
            request.SetRequestHeader("Content-Type", "application/json");
        }
    }
    
}