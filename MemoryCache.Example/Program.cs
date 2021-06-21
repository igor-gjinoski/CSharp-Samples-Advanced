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

            var cacheItem = await cache.GetOrCreateAsync(key, async () => await Task.FromResult("Some value"), token);
            var hit = cacheItem.Hit;
            var value = cacheItem.Value;
        }
    }
}
