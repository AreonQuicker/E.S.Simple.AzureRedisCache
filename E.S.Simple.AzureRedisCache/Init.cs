using E.S.Simple.AzureRedisCache.Core;
using E.S.Simple.AzureRedisCache.Interfaces;
using E.S.Simple.MemoryCache;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace E.S.Simple.AzureRedisCache
{
    public static class Init
    {
        public static void AddAzureRedisCache(this IServiceCollection services, string configuration, string instanceName)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration;
                options.InstanceName = instanceName;
            });

            services.AddSimpleMemoryCache();

            services.AddTransient<IRedisCacheManager, RedisCacheManager>();
            services.AddTransient<IFullCacheManager, FullCacheManager>();
            services.AddTransient<ICacheManagerInstance, CacheManagerInstance>();
        }
    }
}
