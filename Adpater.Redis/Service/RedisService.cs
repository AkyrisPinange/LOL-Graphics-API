using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Redis.Service
{
    public class RedisService
    {
        private readonly IDistributedCache _cache;

        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Cache for 30 minutes
            };

            await _cache.SetStringAsync(key, value, options);
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }
    }
}
