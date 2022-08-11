using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngine.Services;

namespace ChainEngine.Model
{
    public class PlayerNftCollection
    {
        private int _total;
        private readonly int _limit;
        private int _page;

        private readonly PlayerService _service;

        private readonly List<Nft> _items = new List<Nft>();

        public PlayerNftCollection(int limit, int page, PlayerService service)
        {
            _limit = limit;
            _page = page;
            _service = service;
        }

        public List<Nft> Items()
        {
            return _items;
        }

        public bool HasNext()
        {
            return _total > _page * _limit;
        }

        private async Task<List<Nft>> FetchData()
        {
            var response = await _service.GetNFTs(_page, _limit);
            var nfts = response.Items[0].Nfts;

            _total = response.Total;
            _page = response.Page;

            _items.AddRange(nfts);

            return nfts;
        }

        public async Task<PlayerNftCollection> FirstPage()
        {
            await FetchData();
            return this;
        }

        public async Task<List<Nft>> NextPage()
        {
            _page += 1;
            return await FetchData();
        }
    }
}
