using System.Text.Json;
using API.Models;

namespace API.Services
{
    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://hacker-news.firebaseio.com/v0/";

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_apiUrl);
        }

        public async Task<int[]> GetTopItemsAsync()
        {
            var response = await _httpClient.GetAsync("topstories.json");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int[]>(content);
        }

        public async Task<HackerNewsItem> GetItemAsync(int itemId)
        {
            var response = await _httpClient.GetAsync($"item/{itemId}.json");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<HackerNewsItem>(content);
        }
    }
}
