using System;
using System.Runtime.Caching;

namespace ProxyCach
{
    /**
     * Full Generic Proxy Cache
     */
    internal class GenericProxyCache<T> where T : class
    {
        private ObjectCache cache;
        private int dt_seconds = 300; // added to the expiration time

        public T Get(string key, object[] arguments)
        {
            cache = MemoryCache.Default;

            T content = cache[key] as T;

            if (content == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddSeconds(dt_seconds);
                content = Activator.CreateInstance(typeof(T), arguments) as T;

                cache.Set(key, content, policy);
            }

            return content;
        }
    }
}
