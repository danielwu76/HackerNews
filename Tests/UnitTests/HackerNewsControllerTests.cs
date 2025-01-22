using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using API.Models;

namespace Tests.UnitTests
{
    public class HackerNewsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GetTopItems_ReturnsOkWithItemIds()
        {
        
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();
                        
            var response = await client.GetAsync("/api/hackernews");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var itemIds = JsonSerializer.Deserialize<int[]>(content);

            Assert.NotNull(itemIds);
            Assert.True(itemIds.Length > 0);
        }

        [Fact]
        public async Task GetItem_ReturnsOkWithItem()
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var itemId = 1;

            var response = await client.GetAsync($"/api/hackernews/item/{itemId}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<HackerNewsItem>(content);

            Assert.NotNull(item);
            Assert.Equal(itemId, item?.id);
        }
    }
}
