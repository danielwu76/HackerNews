using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly ILogger<HackerNewsController> _logger;
        private readonly HackerNewsService _hackerNewsService;

        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet(Name = "GetTopItems")]
        public async Task<IActionResult> GetTopItems()
        {
            var itemIds = await _hackerNewsService.GetTopItemsAsync();
            return Ok(itemIds);
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _hackerNewsService.GetItemAsync(id);
            return item != null ? Ok(item) : NotFound();
        }
    }
}
