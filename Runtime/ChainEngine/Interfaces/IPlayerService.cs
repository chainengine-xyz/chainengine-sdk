using ChainEngine.Model;
using ChainEngine.Remote.Models;
using Cysharp.Threading.Tasks;

namespace ChainEngine.Interfaces
{
    public interface IPlayerService
    {
        /*
         * Game developer should be able to create a player.
         */
        public UniTask<Player> CreateOrFetch(string walletAddress);

        /*
         * Game developer should be able to fetch player's NFTs.
         */
        public UniTask<NftCallResponse> GetNFTs(int page, int limit);

        /*
         * Developer should be able to fetch an specific NFT.
         */
        public UniTask<Nft> GetNFT(string chainId);

        /*
         * Developer should be able to transfer a NFT from the player's wallet.
         */
        public UniTask<string> TransferNft(string walletAddress, string nftId, int amount);

        /*
         * Developer should be able to authenticate a player.
         */
        public UniTask<string> GetNonce();
    }
}
