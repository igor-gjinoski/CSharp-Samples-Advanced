using MemoryCache.LazyCache;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache.Example
{
    /// <summary>
    /// InMemoryCache is just for Demo
    /// Instead use LazyCache implementation.
    /// MS MemoryCache causes problems when Expiration is less than 20 seconds.
    /// </summary>
    class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddMemoryCache()
                .AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>()
                .BuildServiceProvider();

            var cache = serviceProvider.GetService<IMemoryCacheProvider>();

            await TryInMemoryCache(cache);

            /*
             * Better!
             * Use LazyCache
             */
            await TryLazyCache();
        }


        static async Task TryInMemoryCache(IMemoryCacheProvider cache)
        {
            var key = "key";
            var token = new CancellationTokenSource().Token;

            var cacheItem1 = await cache.GetOrCreateAsync(key,
                async () => await Task.FromResult("Some value"), token,
                new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions()
                {
                    SlidingExpiration = System.TimeSpan.FromSeconds(10),
                    AbsoluteExpirationRelativeToNow = System.TimeSpan.FromSeconds(10)
                });

            var cacheItem2 = await cache.GetOrCreateAsync(key,
                async () => await Task.FromResult("Some value"), token);

            var hit = cacheItem2.Hit;
            var value = cacheItem2.Value;
        }


        /// <summary>
        /// Cache implementation with LazyCache
        /// </summary>
        static async Task TryLazyCache()
        {
            var serviceProvider = new ServiceCollection()
                .AddLazyCacheProvider()
                .BuildServiceProvider();

            var lazyCache = serviceProvider.GetService<ILazyCacheProvider>();

            var key = "test";
            var value = "some text";
            var token = new CancellationTokenSource().Token;

            await lazyCache.GetOrAddAsync(
                value,
                cacheOptions =>
                {
                    cacheOptions.SetSlidingExpiration(new System.TimeSpan(10))
                                .SetAbsoluteExpirationRelativeToNow(new System.TimeSpan(25))
                                .WithKeySelector(key);
                },
                token);
        }
    }
}
