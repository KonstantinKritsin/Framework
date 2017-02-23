using System;
using System.Runtime.Caching;
using Project.Framework.CrossCuttingConcerns.Caching;

namespace Project.Configuration.DefaultConfiguration
{
    public class InMemoryCache : ICache
    {
        private readonly MemoryCache _cache = MemoryCache.Default;

        public void Add(string key, object value, DateTimeOffset absoluteExpiration)
        {
            _cache.Add(key, value, absoluteExpiration);
        }

        public void Add(string key, object value, TimeSpan slidingExpiration)
        {
            _cache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public bool TryGetValue<T>(string key, out object value)
        {
            value = null;
            if (!_cache.Contains(key))
                return false;
            value = _cache[key];
            return true;
        }

        public bool TryGetValue<T>(string key, out T value) where T : class
        {
            value = null;
            if (!_cache.Contains(key))
                return false;
            value = (T)_cache[key];
            return true;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void ClearCache()
        {
            _cache.Trim(100);
        }

        public void Dispose()
        {
        }
    }
}