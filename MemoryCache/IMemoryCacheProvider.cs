using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache
{
    public interface IMemoryCacheProvider
    {
        void Remove(object key);

        Task<(bool Hit, T Value)> GetOrCreateAsync<T>(
            object key,
            Func<Task<T>> getDataAsync,
            CancellationToken cancellationToken);

        Task<(bool Hit, T Value)> GetOrCreateAsync<T>(
            object key,
            Func<Task<T>> getDataAsync,
            CancellationToken cancellationToken,
            MemoryCacheEntryOptions cacheEntryOptions);
    }
}
