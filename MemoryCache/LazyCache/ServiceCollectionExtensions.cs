using Microsoft.Extensions.DependencyInjection;

namespace MemoryCache.LazyCache
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLazyCacheProvider(this IServiceCollection services)
        {
            return services
                    .AddLazyCache()
                    .AddSingleton<ILazyCacheProvider, LazyCacheProvider>();
        }
    }
}
