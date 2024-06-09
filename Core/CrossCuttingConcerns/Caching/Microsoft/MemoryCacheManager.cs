using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager(IMemoryCache memoryCache) : ICacheManager
{
    public T? Get<T>(string key)
    {
        return memoryCache.Get<T>(key);
    }

    public object? Get(string key)
    {
        return memoryCache.Get(key);
    }

    public void Add(string key, object data, int duration = 60)
    {
        memoryCache.Set(key, data, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return memoryCache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
        var cacheEntriesCollection = cacheEntriesCollectionDefinition?.GetValue(memoryCache) as dynamic;
        var cacheCollectionValues = new List<ICacheEntry>();
        if (cacheEntriesCollection != null)
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry? cacheItemValue = cacheItem.GetType().GetProperty("Value")?.GetValue(cacheItem, null);
                if (cacheItemValue != null) cacheCollectionValues.Add(cacheItemValue);
            }

        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString()!)).Select(d => d.Key).ToList();
        foreach (var key in keysToRemove)
        {
            memoryCache.Remove(key);
        }
    }
}