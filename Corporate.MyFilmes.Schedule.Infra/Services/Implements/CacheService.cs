using Corporate.MyFilmes.Schedule.Infra.Services.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.Services.Implements
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);

            return value is null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<T> SetAsync<T>(string key, T value, DateTimeOffset? timeToLive = null)
        {
            if (timeToLive is null)
            {
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value));
                return value;
            }

            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = timeToLive
            });

            return value;
        }
    }
}
