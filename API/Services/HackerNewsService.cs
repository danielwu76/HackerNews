using System.Net.Http.Json;
using System.Text.Json;
using API.Models;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public class HackerNewsService
    {
        private readonly string _apiUrl = "https://hacker-news.firebaseio.com/v0/";
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public HackerNewsService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_apiUrl);
            _cache = cache;
        }

        public async Task<IEnumerable<int>> GetTopItemsAsync()
        {
            const string cacheKey = "GetTopItems";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<int> itemIds))
            {
                itemIds = await _httpClient.GetFromJsonAsync<IEnumerable<int>>(_apiUrl + "topstories.json");

                if (itemIds.Any())
                {
                    _cache.Set(cacheKey, itemIds, _cacheDuration);
                }
            }

            return itemIds ?? Enumerable.Empty<int>();
        }

        public async Task<HackerNewsItem> GetItemAsync(int itemId)
        {
            string cacheKey = $"Item_{itemId}";

            if(!_cache.TryGetValue(cacheKey,out HackerNewsItem? hackerNewsItem))
            {
                hackerNewsItem = await _httpClient.GetFromJsonAsync<HackerNewsItem>(_apiUrl + "item/" + itemId + ".json");

                if (hackerNewsItem != null)
                {
                    _cache.Set(cacheKey, hackerNewsItem, _cacheDuration);
                }
            }

            return hackerNewsItem;
        }
    }
}
