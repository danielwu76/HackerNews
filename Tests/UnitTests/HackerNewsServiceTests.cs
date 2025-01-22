using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using Moq;
using API.Models;
using API.Services;

namespace Tests.UnitTests
{
    public class HackerNewsServiceTests
    {
        [Fact]
        public async Task GetTopItemsAsync_ReturnsItemIds()
        {
            var handler = new TestMessageHandler();
            var client = new HttpClient(handler);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new HackerNewsService(client, memoryCache);
            var mockItemIds = new[] { 1, 2, 3, 4, 5 };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonSerializer.Serialize(mockItemIds)) };
            
            handler.SetResponse(responseMessage);

            var result = await service.GetTopItemsAsync();

            Assert.NotNull(result);
            Assert.Equal(mockItemIds, result);
        }

        [Fact]
        public async Task GetItemAsync_ReturnsItem()
        {
            var handler = new TestMessageHandler();
            var client = new HttpClient(handler);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new HackerNewsService(client, memoryCache);
            var mockItem = new HackerNewsItem { id = 1, title = "Test Title", by = "Test Author", url = "http://example.com" };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonSerializer.Serialize(mockItem)) };
           
            handler.SetResponse(responseMessage);

            var result = await service.GetItemAsync(1);

            Assert.NotNull(result);
            Assert.Equal(mockItem.id, result?.id);
            Assert.Equal(mockItem.title, result?.title);
        }
    }
}
