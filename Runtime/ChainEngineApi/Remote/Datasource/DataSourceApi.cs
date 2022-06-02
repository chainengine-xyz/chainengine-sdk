using System;
using System.Collections.Generic;
using ChainEngineSDK.ChainEngineApi.Client;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Interfaces;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using ChainEngineSDK.ChainEngineApi.Shared.Exceptions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ChainEngineSDK.ChainEngineApi.Remote.Datasource
{
    public class DataSourceApi: IDataSourceApi
    {
        private const string ServerURL = "http://localhost:3000/";

        private ApiClient _client;

        public DataSourceApi(ApiClient client)
        {
            _client = client;
        }

        public UniTask<Player> GetPlayerByWallet(string wallet)
        {
            // Waiting new player APIs.
            throw new NotImplementedException();
        }

        public async UniTask<List<RemoteNFT>> GetNFTsByPlayer(string wallet)
        {
            var www = new UnityWebRequest(ServerURL + "accounts/" + _client.GetAccountId() + "/nfts?fetchFor=player&playerId=test", "GET");
            
            PreflightHeader(www);

            www.downloadHandler = new DownloadHandlerBuffer();

            var nfts = new List<RemoteNFT>();
            
            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);

                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<RemoteNFTCallResponse>(req.downloadHandler.text);
                nfts = res.Items[0].nfts;
            }
            catch (Exception)
            {
                throw new UnableToLoadNFTs();
            }

            return nfts;
        }

        public async UniTask<Player> CreatePlayer(Player playerDto)
        {
            var json = JsonUtility.ToJson(playerDto);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

            var www = new UnityWebRequest(ServerURL + "accounts/" + _client.GetAccountId() + "/players", "POST");
        
            PreflightHeader(www);
        
            www.uploadHandler = new UploadHandlerRaw(jsonEncoded);
            www.downloadHandler = new DownloadHandlerBuffer();

            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);
            }
            catch (Exception)
            {
                throw new PlayerNotCreated();
            }

            return playerDto;
        }

        public async UniTask<Nft> MintNFT(Nft nft)
        {
            var listOfNFTs = new List<Nft> { nft };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(listOfNFTs);
            var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

            var www = new UnityWebRequest(
                ServerURL + "accounts/" + _client.GetAccountId() + "/nfts/game/" + nft.GameId, 
                "POST");

            PreflightHeader(www);
        
            www.uploadHandler = new UploadHandlerRaw(jsonEncoded);
            www.downloadHandler = new DownloadHandlerBuffer();

            try
            {
                var req = await www.SendWebRequest();
                Debug.Log(req.downloadHandler.text);
            }
            catch (Exception)
            {
                throw new UnableToCreateNFT();
            }

            return nft;
        }
        
        private void PreflightHeader(UnityWebRequest request) {
            request.SetRequestHeader("x-api-key", _client.GetApiKey());
            request.SetRequestHeader("x-api-secret", _client.GetSecret());
            request.SetRequestHeader("Content-Type", "application/json");
        }
    }
    
}