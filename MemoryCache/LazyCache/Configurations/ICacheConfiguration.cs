using System;

namespace MemoryCache.LazyCache.Configurations
{
    public interface ICacheConfiguration
    {
        ICacheConfiguration SetSlidingExpiration(TimeSpan slidingExpiration);

        ICacheConfiguration SetAbsoluteExpirationRelativeToNow(TimeSpan absoluteExpiration);

        ICacheConfiguration WithKeySelector(string cacheKeySelector);
    }
}
