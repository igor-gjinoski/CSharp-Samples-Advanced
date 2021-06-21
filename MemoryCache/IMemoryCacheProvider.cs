using System;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache
{
    public interface IMemoryCacheProvider
    {
        void Remove(object key);

        (bool Hit, T Value) GetOrCreate<T>(object key, Func<Task<T>> getDataAsync, DateTimeOffset refreshInterval = default);

        Task<(bool Hit, T Value)> GetOrCreateAsync<T>(object key, Func<Task<T>> getDataAsync,
            CancellationToken cancellationToken,
            DateTimeOffset refreshInterval = default);
    }
}
