using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E.S.Simple.AzureRedisCache.Interfaces
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(string key) where T : class;      
        Task SetAsync<T>(string key, T value, int? cacheTimeInSeconds = null) where T : class;      
        Task RemoveAsync(string key);
    }
}
