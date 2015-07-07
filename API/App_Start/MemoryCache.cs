using System;

namespace API
{
    /// <summary>
    /// Allows to cache entries to system memory.
    /// </summary>
    public static class MemoryCache
    {
        /// <summary>
        /// Searches for an entry in the cache.
        /// </summary>
        /// <param name="key">The unique key of the entry.</param>
        /// <returns>The cached dictionary element.</returns>
        public static object GetValue(string key)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;
            return memoryCache.Get(key);
        }

        /// <summary>
        /// Added an entry to the cache.
        /// </summary>
        /// <param name="key">The unique key of the entry.</param>
        /// <param name="value">The value to hold.</param>
        /// <param name="absExpiration">The time at which the cache is set to expire.</param>
        /// <returns>The status of the operation.</returns>
        public static bool Add(string key, object value, DateTimeOffset absExpiration)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);
        }

        /// <summary>
        /// Removes the entry from the cache.
        /// </summary>
        /// <param name="key">The unique key of the entry.</param>
        public static void Delete(string key)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;
            if (memoryCache.Contains(key)) memoryCache.Remove(key);
        }
    }
}