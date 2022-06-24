using System.Collections.Generic;
using System.Threading.Tasks;
using ChainEngineSDK.ChainEngineApi.Services;

namespace ChainEngineApi.Model
{
    public class PlayerNftCollection
    {
        private int _total = 0;
        private int _limit = 10;
        private int _page = 1;
        
        private readonly ApiService _service;

        private readonly List<Nft> _items = new List<Nft>();

        public PlayerNftCollection(int limit, int page, ApiService service)
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
            var response = await _service.GetPlayerNFTs(_page, _limit);
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