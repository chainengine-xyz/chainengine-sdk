using System.Collections;
using ChainEngineSDK.ChainEngineApi.Model;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.ChainEngineApi.Remote.Interfaces
{
    public interface IDataSourceApi
    {
	    public UniTask<string> GetPlayerByWallet(string wallet);
	    public UniTask<string> GetNFTsByPlayer(string wallet);

		public UniTask<Player> CreatePlayer(Player dto);
		public UniTask<Nft> CreateNft(Nft nft);
    }
}