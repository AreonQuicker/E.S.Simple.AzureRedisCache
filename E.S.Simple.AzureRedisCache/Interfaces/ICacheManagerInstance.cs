namespace E.S.Simple.AzureRedisCache.Interfaces
{
    public interface ICacheManagerInstance
    {
        ICacheManager Make(bool includeMemoryCaching = true);
    }
}
