using Microsoft.Extensions.Caching.Memory;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public sealed class MemoryCacheManager(IMemoryCache memoryCache) : ICacheManager
{
    private readonly HashSet<string> _cacheKeys = [];
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
        _cacheKeys.Add(key);
    }

    public bool IsAdd(string key)
    {
        return memoryCache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
        _cacheKeys.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var keysToRemove = _cacheKeys.Where(key => key.Contains(pattern)).ToList();
        foreach (var key in keysToRemove)
        {
            Remove(key);
        }
    }
}