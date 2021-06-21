using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddMemoryCache()
                .AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>()
                .BuildServiceProvider();

            var cache = serviceProvider.GetService<IMemoryCacheProvider>();

            await TryCache(cache);
        }

        static async Task TryCache(IMemoryCacheProvider cache)
        {
            var key = "key";
            var token = new CancellationTokenSource().Token;

            var cacheItem1 = await cache.GetOrCreateAsync(key, 
                async () => await Task.FromResult("Some value"), token, System.DateTimeOffset.MaxValue);

            var cacheItem2 = await cache.GetOrCreateAsync(key, 
                async () => await Task.FromResult("Some value"), token, System.DateTimeOffset.MaxValue);

            var hit = cacheItem2.Hit;
            var value = cacheItem2.Value;
        }
    }
}
