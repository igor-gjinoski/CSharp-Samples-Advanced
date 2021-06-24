using MemoryCache.LazyCache.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCache.LazyCache
{
    public class LazyCacheProvider : ILazyCacheProvider
    {
        private readonly IAppCache _appCache;

        public LazyCacheProvider(IAppCache appCache)
        {
            _appCache = appCache;
        }

        public Unit RemoveAsync<TValue>(string value)
        {
            _appCache.Remove(value);

            return Unit.Value;
        }

        public async Task<TValue> GetOrAddAsync<TValue>(
            TValue value,
            Action<ICacheConfiguration> action,
            CancellationToken token)
        {
            var cacheConfiguration = new CacheConfiguration();
            action?.Invoke(cacheConfiguration);

            ICachePolicyConfigurator cacheConfigurations = cacheConfiguration.Build();

            var cacheKey = GetCacheKeySelector(cacheConfigurations);
            var cachePolicy = CreateCacheEntryOptions(cacheConfigurations);

            return await _appCache
                .GetOrAddAsync(cacheKey,
                    async () => await Task.FromResult(value),
                    cachePolicy ?? default);
        }

        #region PRIVATE

        private string GetCacheKeySelector(ICachePolicyConfigurator cacheConfigurator)
        {
            return cacheConfigurator.CacheKeySelector ?? throw new ArgumentNullException();
        }


        private MemoryCacheEntryOptions CreateCacheEntryOptions(ICachePolicyConfigurator cacheConfigurator)
        {
            return new MemoryCacheEntryOptions()
            {
                SlidingExpiration = cacheConfigurator?.SlidingExpiration,
                AbsoluteExpirationRelativeToNow = cacheConfigurator?.AbsoluteExpiration
            };
        }

        #endregion
    }
}
