using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCache
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks;

        /// <summary>
        /// SemaphoreSlim
        /// Limits the number of threads that can access a resource concurrently.
        /// </summary>
        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache;
            _locks = new ConcurrentDictionary<object, SemaphoreSlim>();
        }

        /// <summary>
        /// Remove value from cache by specific key.
        /// </summary>
        /// <param name="key">Cache key</param>
        public void Remove(object key)
        {
            _cache.Remove(key);
        }


        /// <summary>
        /// Get value Async from cache
        /// or create it Async if not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="getDataAsync">Set this data if value not exist.</param>
        /// <param name="refreshInterval">Lifetime of the cahed value</param>
        public async Task<(bool Hit, T Value)> GetOrCreateAsync<T>(object key, Func<Task<T>> getDataAsync, 
            CancellationToken cancellationToken, 
            DateTimeOffset refreshInterval = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(GetOrCreate<T>(key, getDataAsync, refreshInterval));
        }


        /// <summary>
        /// Get value from cache
        /// or create it if not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="getDataAsync">Set this data if value not exist.</param>
        /// <param name="refreshInterval">Lifetime of the cahed value</param>
        public (bool Hit, T Value) GetOrCreate<T>(object key, Func<Task<T>> getDataAsync, DateTimeOffset refreshInterval = default)
        {
            bool cacheHit = _cache.TryGetValue(key, out T value);
            if (cacheHit)
            {
                return (cacheHit, value);
            }

            SemaphoreSlim @lock =
                _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

            @lock.Wait();

            try
            {
                if (!_cache.TryGetValue(key, out value))
                {
                    value = getDataAsync
                        .Invoke()
                        .Result;
                    _cache.Set(key, value, refreshInterval);
                }
            }
            finally
            {
                @lock.Release();
            }
            return (true, value);
        }
    }
}
