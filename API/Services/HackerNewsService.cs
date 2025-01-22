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

        // Get top stories
        public async Task<int[]> GetTopStoriesAsync()
        {
            var response = await _httpClient.GetAsync("topstories.json");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int[]>(content);
        }

        // Get details of a specific story
        public async Task<HackerNewsItem> GetStoryAsync(int storyId)
        {
            var response = await _httpClient.GetAsync($"item/{storyId}.json");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<HackerNewsItem>(content);
        }
    }
}
