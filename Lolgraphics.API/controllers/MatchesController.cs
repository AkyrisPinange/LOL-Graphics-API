using Adaper.Kafka.Producer;
using Adapter.Redis.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly ProducerService producerService;
    private readonly RedisService redisService;

    public MatchesController(ProducerService producerService, RedisService redisService)
    {
        this.redisService = redisService;
        this.producerService = producerService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMatches(CancellationToken cancellationToken)
    {
        await producerService.ProducerAsync(cancellationToken);
        return Ok();
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetCacheValue(string key)
    {
        var value = await redisService.GetCacheValueAsync(key);
        if (value == null)
        {
            return NotFound();
        }
        return Ok(value);
    }

    [HttpPost("cache")]
    public async Task<IActionResult> SetCacheValue(string key, [FromBody] string value)
    {
        await redisService.SetCacheValueAsync(key, value);
        return Ok();
    }
}
