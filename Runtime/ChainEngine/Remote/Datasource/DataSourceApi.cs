using System;
using ChainEngine.Client;
using ChainEngine.Model;
using ChainEngine.Remote.Interfaces;
using ChainEngine.Remote.Models;
using ChainEngine.Shared.Exceptions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace ChainEngine.Remote.Datasource
{
    public class DataSourceApi: IDataSourceApi
    {
        public static readonly string ServerURL = "https://api.chainengine.xyz";
        public static readonly string UiURL = "https://console.chainengine.xyz";

        private readonly ChainEngineSDK sdkClient;

        public DataSourceApi(ChainEngineSDK client)
        {
            sdkClient = client;
        }

        public async UniTask<Player> CreateOrFetchPlayer(NewPlayerRequest playerDto)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(playerDto);

                var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

                var req = await SendRequest("/clientapp/players", "POST", jsonEncoded);

                var newPlayer = Newtonsoft.Json.JsonConvert.DeserializeObject<NewPlayerResponse>(req.downloadHandler.text);

                if (newPlayer == null) throw new Exception("Invalid response from api");

                return newPlayer.Player;
            }
            catch (Exception exception)
            {
                throw new PlayerNotCreated(exception.Message, exception);
            }
        }

        public async UniTask<NftCallResponse> GetPlayerNFTs(int page, int limit)
        {
            try
            {
                var req = await SendRequest("/clientapp/players/nfts?queryBy=player&limit=" + limit + "&page=" + page, "GET");

                NftCallResponse callResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<NftCallResponse>(req.downloadHandler.text);
                
                return callResponse;
            } catch (Exception exception)
            {
                throw new UnableToLoadNFTs(exception.Message, exception);
            }
        }

        public async UniTask<Nft> GetPlayerNFT(string id)
        {
            try
            {
                var req = await SendRequest("/clientapp/players/nfts/" + id, "GET");

                var nft = Newtonsoft.Json.JsonConvert.DeserializeObject<Nft>(req.downloadHandler.text);
                
                return nft;
            }
            catch (Exception exception)
            {
                throw new UnableToLoadNFTs(exception.Message, exception);
            }
        }

        public async UniTask<string> GetNonce()
        {
            try
            {
                var nonce = new NonceRequest{
                    GameId = sdkClient.GameId
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(nonce);

                var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

                var req = await SendRequest("/clientapp/auth/nonce", "POST", jsonEncoded);
                
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<NonceResponse>(req.downloadHandler.text);

                if (data == null) throw new Exception("Invalid response from api");

                return data.Nonce;
            }
            catch (Exception exception)
            {
                throw new PlayerNotCreated(exception.Message, exception);
            }
        }
        
        public async UniTask<string> TransferNft(SignedTransferRequest transfer)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(transfer);

                var jsonEncoded = new System.Text.UTF8Encoding().GetBytes(json);

                var req = await SendRequest("/clientapp/players/nfts/transfer", "POST", jsonEncoded);
                
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<SignedTransferResponse>(req.downloadHandler.text);

                if (data == null) throw new Exception("Invalid response from api");

                return data.Id;
            }
            catch (Exception exception)
            {
                throw new PlayerNotCreated(exception.Message, exception);
            }
        }

        private async UniTask<UnityWebRequest> SendRequest(string url, string method, [CanBeNull] byte[] encodedData = null)
        {
            var www = new ChainEngineWebClient(sdkClient.Player?.Token, ServerURL + url, method, sdkClient.ApiMode);

            if (encodedData != null)
            {
                www.uploadHandler = new UploadHandlerRaw(encodedData);
            }

            www.downloadHandler = new DownloadHandlerBuffer();

            return await www.SendWebRequest();
        }
    }
}
