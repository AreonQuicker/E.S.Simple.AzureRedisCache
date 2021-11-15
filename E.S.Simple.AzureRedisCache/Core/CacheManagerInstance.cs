using E.S.Simple.AzureRedisCache.Interfaces;
using System;

namespace E.S.Simple.AzureRedisCache.Core
{
    public class CacheManagerInstance : ICacheManagerInstance
    {

        #region MyRegion   .
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        public CacheManagerInstance(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        #endregion

        public ICacheManager Make(bool includeMemoryCaching = true)
        {
            if (includeMemoryCaching)
                return (IFullCacheManager)_serviceProvider.GetService(typeof(IFullCacheManager));

            return (IRedisCacheManager)_serviceProvider.GetService(typeof(IRedisCacheManager));
        }
    }
}
