using MemoryCache.LazyCache.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache.LazyCache
{
    public interface ILazyCacheProvider
    {
        Unit RemoveAsync<TValue>(string value);

        Task<TValue> GetOrAddAsync<TValue>(
            TValue value,
            Action<ICacheConfiguration> action,
            CancellationToken token);
    }
}
