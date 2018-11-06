using HearstMappingsEditor.Common.Interfaces;
using System;
using System.Runtime.Caching;

namespace HearstMappingsEditor.Common
{
    public class InMemoryCache : ICache
    {
        protected readonly MemoryCache _cache;

        public InMemoryCache()
        {
            _cache = MemoryCache.Default;
        }

        public bool RemoveItem(string key)
        {
            return _cache.Remove(key) != null;
        }

        public TItem GetItem<TItem>(string key) where TItem : class
        {
            return _cache[key] as TItem;
        }

        public TItem GetItem<TItem>(string key, TimeSpan expiration, Func<TItem> funcGetter) where TItem : class
        {
            TItem item = (TItem)_cache[key];

            if (item == null && funcGetter != null)
            {
                item = funcGetter();
                if (item != null)
                {
                    _cache.Add(key, item, DateTime.Now.Add(expiration));
                }
            }

            return item;
        }

        public void SetOrUpdateItem<TItem>(string key, TimeSpan expiration, TItem value) where TItem : class
        {
            _cache.Set(key, value, DateTime.Now.Add(expiration));
        }
    }
}
