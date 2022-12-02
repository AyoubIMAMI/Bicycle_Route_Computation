using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace ProxyCach
{
    internal class GenericProxyCache<T> where T : class
    {
        private ObjectCache cache;
        private int dt_seconds = 300;

        public GenericProxyCache()
        {
            cache = MemoryCache.Default;
        }

        private T Get(string key, object[] arguments)
        {
            T content = cache[key] as T;

            if (content == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddSeconds(dt_seconds);
                content = (T)Activator.CreateInstance(typeof(T), arguments);

                cache.Set(key, content, policy);
            }

            return content;
        }
    }
}
