using System.Collections;
using System.Collections.Generic;
using ChainEngineSDK.ChainEngineApi.Model;
using ChainEngineSDK.ChainEngineApi.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.ChainEngineApi.Remote.Interfaces
{
    public interface IDataSourceApi
    {
	    public UniTask<string> GetPlayerByWallet(string wallet);
	    public UniTask<List<RemoteNFT>> GetNFTsByPlayer(string wallet);

		public UniTask<Player> CreatePlayer(Player dto);
		public UniTask<Nft> CreateNft(Nft nft);
    }
}