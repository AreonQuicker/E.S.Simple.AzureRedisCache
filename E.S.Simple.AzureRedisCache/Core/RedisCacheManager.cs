using E.S.Simple.AzureRedisCache.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E.S.Simple.AzureRedisCache.Core
{
    public class RedisCacheManager : IRedisCacheManager
    {
        #region MyRegion
        private const int _cacheTimeInSeconds = 300;
        private readonly IDistributedCache _distributedCache;
        #endregion

        #region Constructor
        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        #endregion

        #region ICacheManager   

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var json = await _distributedCache.GetStringAsync(key);

            if (json == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, int? cacheTimeInSeconds = null) where T : class
        {
            if (value == null)
                return;

            var json = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(
                key, json,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTimeInSeconds ?? _cacheTimeInSeconds) });
        }
        #endregion
    }
}
