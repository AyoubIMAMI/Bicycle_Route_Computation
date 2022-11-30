using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace ProxyCache
{
    internal class GenericProxyCache<T> where T : class
    {
        private ObjectCache cache;
        private int dt_seconds = 300;

        public GenericProxyCache()
        {
            cache = MemoryCache.Default;
        }

        public T Get(string key)
        {
            return null;
        }

        public T Get(string key, object[] arguments)
        {
            return null;
        }

        private T moijmenfiche(string key, object[] arguments)
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
