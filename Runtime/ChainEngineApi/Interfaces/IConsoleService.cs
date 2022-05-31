using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Model;
using Cysharp.Threading.Tasks;

namespace ChainEngineSDK.ChainEngineApi.Interfaces
{
    public interface IConsoleService
    {
        public UniTask<Player> CreatePlayer(string walletAddress);
        public UniTask<Nft> CreateNft(NftMetadata nft);
    }
}