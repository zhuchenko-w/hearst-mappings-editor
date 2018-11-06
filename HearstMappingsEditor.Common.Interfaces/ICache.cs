using System;

namespace HearstMappingsEditor.Common.Interfaces
{
    public interface ICache
    {
        bool RemoveItem(string key);
        TItem GetItem<TItem>(string key) where TItem : class;
        TItem GetItem<TItem>(string key, TimeSpan expiration, Func<TItem> funcGetter) where TItem : class;
        void SetOrUpdateItem<TItem>(string key, TimeSpan expiration, TItem value) where TItem : class;
    }
}
