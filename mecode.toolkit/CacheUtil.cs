using System.Runtime.Caching;

namespace mecode.toolkit
{
    public class CacheUtil
    {
        //缓存池
        private static ObjectCache cache = MemoryCache.Default;
        public static void Insert(string key, object value)
        {
            cache[key] = value;
        }

        public static object Get(string key)
        {
            return cache[key];
        }
    }
}
