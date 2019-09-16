using Core.Infrastructure.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.RuntimeCache
{
    public sealed class CacheProvider : ICacheProvider
    {
        #region Fields

        private readonly IMemoryCache _cache;

        #endregion

        #region Constructors and Destructors

        private CacheProvider(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        #endregion

        #region Public Methods and Operators

        public void Add<T>(string key, T data, double expiry = 6) where T : class
        {
            DateTime cacheEntry;

            // Look for cache key.
            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = DateTime.Now;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(expiry));

                // Save data in cache.
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
        }

        public bool ContainsKey(string key)
        {
            return _cache.Get<object>(key) != null;
        }

        public T Get<T>(string key, double expiry = 6) where T : class
        {
            var cacheEntry = _cache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(expiry);
                return entry.Value as T;
            });
            return cacheEntry;
        }

        public async Task<T> GetAsync<T>(string key, double expiry = 6) where T : class
        {
            var cacheEntry = await
            _cache.GetOrCreateAsync(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(expiry);
                return Task.FromResult(entry.Value as T);
            });
            return cacheEntry;
        }

        public IEnumerable<string> GetAllCacheKeys()
        {
            var field = typeof(IMemoryCache).GetProperty("EntriesCollection", 
                                BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(_cache) as ICollection;
            var items = new List<string>();
            if (collection != null)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    items.Add(val.ToString());
                }
            return items;
        }

        public void Purge()
        {
            foreach (var key in GetAllCacheKeys())
            {
                _cache.Remove(key);
            }
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        #endregion
    }
}
