using E.S.Simple.AzureRedisCache.Interfaces;
using E.S.Simple.MemoryCache.Interfaces;
using System.Threading.Tasks;

namespace E.S.Simple.AzureRedisCache.Core
{
    public class FullCacheManager : IFullCacheManager
    {
        #region MyRegion
        private const int _cacheTimeInSeconds = 300;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IMemoryCacheManager _memoryCacheManager;
        #endregion

        #region Constructor
        public FullCacheManager(IRedisCacheManager redisCacheManager, IMemoryCacheManager memoryCacheManager)
        {
            this._redisCacheManager = redisCacheManager;
            this._memoryCacheManager = memoryCacheManager;
        }
        #endregion

        #region ICacheManager   

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var result = _memoryCacheManager.Get<T>(key);

            if (result != null)
                return result;

            result = await _redisCacheManager.GetAsync<T>(key);

            return result;
        }

        public async Task RemoveAsync(string key)
        {
            _memoryCacheManager.Remove(key);

            await _redisCacheManager.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, int? cacheTimeInSeconds = null) where T : class
        {
            _memoryCacheManager.Set(key, value, cacheTimeInSeconds ?? _cacheTimeInSeconds);

            await _redisCacheManager.SetAsync<T>(key, value, cacheTimeInSeconds);
        }
        #endregion
    }
}
