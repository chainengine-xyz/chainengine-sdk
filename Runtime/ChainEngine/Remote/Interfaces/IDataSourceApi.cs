using ChainEngine.Model;
using ChainEngine.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngine.Remote.Interfaces
{
    public interface IDataSourceApi
    {
        /*
         * Game developer should be able to create a player.
         */
        public UniTask<Player> CreateOrFetchPlayer(NewPlayerRequest dto);

        /*
         * Game developer should be able to fetch player's NFTs.
         */
        public UniTask<NftCallResponse> GetPlayerNFTs(int page, int limit);

        /*
         * Developer should be able to fetch an specific NFT.
         */
        public UniTask<Nft> GetPlayerNFT(string id);

        public UniTask<string> GetNonce();
    }
}
