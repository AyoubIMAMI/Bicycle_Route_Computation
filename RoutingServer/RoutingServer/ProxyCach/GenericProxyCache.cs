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
