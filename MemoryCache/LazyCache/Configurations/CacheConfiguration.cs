using System;

namespace MemoryCache.LazyCache.Configurations
{
    public class CacheConfiguration : ICacheConfiguration
    {
        private TimeSpan _slidingExpiration;
        private TimeSpan _absoluteExpiration;
        private string _cacheKeySelector;


        public ICacheConfiguration SetSlidingExpiration(TimeSpan slidingExpiration)
        {
            _slidingExpiration = slidingExpiration;
            return this;
        }

        public ICacheConfiguration SetAbsoluteExpirationRelativeToNow(TimeSpan absoluteExpiration)
        {
            _absoluteExpiration = absoluteExpiration;
            return this;
        }

        public ICacheConfiguration WithKeySelector(string cacheKeySelector)
        {
            _cacheKeySelector = cacheKeySelector
                ?? throw new ArgumentNullException($"Provide cache key for: {nameof(cacheKeySelector)}");
            return this;
        }

        public ICachePolicyConfigurator Build()
        {
            return new CachePolicyConfigurator(
                _slidingExpiration,
                _absoluteExpiration,
                _cacheKeySelector);
        }
    }

    public interface ICachePolicyConfigurator
    {
        public TimeSpan SlidingExpiration { get; set; }

        public TimeSpan AbsoluteExpiration { get; set; }

        public string CacheKeySelector { get; set; }
    }

    public class CachePolicyConfigurator : ICachePolicyConfigurator
    {
        public TimeSpan SlidingExpiration { get; set; }
        public TimeSpan AbsoluteExpiration { get; set; }
        public string CacheKeySelector { get; set; }

        public CachePolicyConfigurator(
            TimeSpan slidingExpiration, 
            TimeSpan absoluteExpiration, 
            string cacheKeySelector)
        {
            SlidingExpiration = slidingExpiration;
            AbsoluteExpiration = absoluteExpiration;
            CacheKeySelector = cacheKeySelector;
        }
    }
}
